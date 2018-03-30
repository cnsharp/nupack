using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.NuGet;
using CnSharp.VisualStudio.NuPack.Util;
using Microsoft.VisualStudio.Shell;
using Package = Microsoft.VisualStudio.Shell.Package;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddNuSpecCommand : ICommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1eefc81f-e74a-427a-a9e5-671d321226a0");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNuSpecCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddNuSpecCommand(Package package)
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
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        public AddNuSpecCommand()
        {
            
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddNuSpecCommand Instance
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
            Instance = new AddNuSpecCommand(package);
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
            //string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            //string title = "AddNuSpecCommand";

            //// Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(
            //    this.ServiceProvider,
            //    message,
            //    title,
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

          Execute();
        }

        public void Execute(object arg = null)
        {
            var dte = Host.Instance.Dte2;
            var project = dte.GetActiveProejct();
            Common.CheckTfs(project);
            var file = Path.Combine(project.GetDirectory(),NuGetDomain.NuSpecFileName);
            if (File.Exists(file))
                return;

            using (var sw = new StreamWriter(file, false, Encoding.UTF8))
            {
                var temp = Resource.NuSpecTemplate;
                var configFile = Path.Combine(Path.GetDirectoryName(project.FileName), "packages.config");
                if (File.Exists(configFile))
                {
                    try
                    {
                        var reader = new PackagesConfigReader(configFile);
                        var ds = reader.GetDependencySet();
                        if (ds.Any())
                        {
                            var package = new NuGet.Package();
                            package.Metadata.Dependencies = ds;
                            var packageXml = XmlSerializerHelper.GetXmlStringFromObject(package);
                            var start = packageXml.IndexOf("<dependencies>");
                            var end = packageXml.IndexOf("</dependencies>") + "</dependencies>".Length;
                            packageXml = packageXml.Substring(start, end-start);
                            temp = Regex.Replace(temp, "<dependencies>.*?</dependencies>",
                                $"{packageXml}",RegexOptions.Singleline|RegexOptions.Compiled);
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                sw.Write(temp);
                sw.Flush();
                sw.Close();
            }

            project.ProjectItems.AddFromFile(file);
            dte.ItemOperations.OpenFile(file);
        }
    }
}
