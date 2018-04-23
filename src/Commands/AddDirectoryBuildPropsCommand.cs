using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.Extensions;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddDirectoryBuildPropsCommand : ICommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 253;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("0A4174DB-9A22-4CE5-A1CB-B7F63855308F");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        public AddDirectoryBuildPropsCommand()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDirectoryBuildPropsCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddDirectoryBuildPropsCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new OleMenuCommand(this.MenuItemCallback, menuCommandID);
                menuItem.BeforeQueryStatus += MenuItemOnBeforeQueryStatus;
                commandService.AddCommand(menuItem);
            }
        }

        private void MenuItemOnBeforeQueryStatus(object sender, EventArgs e)
        {
            var sln = Host.Instance.DTE.Solution;
            if (sln == null) return;
            var cmd = (OleMenuCommand)sender;
            //cmd.Visible = sln.Projects.Cast<Project>().Any(p => !string.IsNullOrWhiteSpace(p.FileName) && p.IsSdkBased()) && !File.Exists(sln.GetDirectoryBuildPropsPath());
            cmd.Visible = SolutionDataCache.Instance.GetSolutionProperties(sln.FileName).HasSdkBasedProjects && !File.Exists(sln.GetDirectoryBuildPropsPath());
        }


        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddDirectoryBuildPropsCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new AddDirectoryBuildPropsCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            Execute(sender);
        }


        public void Execute(object arg = null)
        {
            var dte = Host.Instance.DTE;
            var sln = dte.Solution;
            if (sln == null) return;
            var file = sln.GetDirectoryBuildPropsPath();
            var sln2 = sln as Solution2;
            if (File.Exists(file))
            {
                try
                {
                    sln2.AddSolutionItem(file);
                }
                finally
                {
                    dte.ItemOperations.OpenFile(file);
                }
                return;
            }

            using (var sw = new StreamWriter(file, false, Encoding.UTF8))
            {
                var temp = Resource.DirectoryBuildPropsTemplate;
                temp = temp.Replace("$company$", Common.GetOrganization() ?? Environment.UserName);
                temp = temp.Replace("$product$", Path.GetFileNameWithoutExtension(sln.FileName));
                sw.Write(temp);
                sw.Flush();
                sw.Close();
            }

            sln2.AddSolutionItem(file);
            dte.ItemOperations.OpenFile(file);
        }
    }
}
