using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.AssemblyInfoEditor;
using CnSharp.VisualStudio.NuPack.Extensions;
using Microsoft.VisualStudio.Shell;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AssemblyInfoEditCommand : ICommand
    {
        public AssemblyInfoEditCommand()
        {

        }


        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 255;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("4005AFC6-1B77-4227-A90E-13A792A6EBB5");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoEditCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AssemblyInfoEditCommand(Package package)
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
            var dte = Host.Instance.Dte2;
            if (dte == null) return;
            var cmd = (OleMenuCommand) sender;
            cmd.Visible =  SolutionDataCache.Instance.GetSolutionProperties(dte.Solution.FileName).HasClassicProjects;
        }



        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AssemblyInfoEditCommand Instance
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
            Instance = new AssemblyInfoEditCommand(package);
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
           Execute();
        }


        public void Execute(object arg = null)
        {
            var dte = Host.Instance.Dte2;
            try
            {
                var sp = SolutionDataCache.Instance.GetSolutionProperties(dte.Solution.FileName);
                var allProjects = sp.Projects;
                if (!allProjects.Any())
                {
                    ServiceProvider.ShowError("No project is opened.",Common.ProductName);
                    return;
                }
                if (!sp.HasClassicProjects)
                {
                    ServiceProvider.ShowError("No project is based on .net framework.", Common.ProductName);
                    return;
                }
                new AssemblyInfoForm(sp.ClassicProjects).ShowDialog();
            }
            catch (Exception exception)
            {
                ServiceProvider.ShowError(exception.Message, Common.ProductName);
            }
        }
    }
}
