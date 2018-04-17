//------------------------------------------------------------------------------
// <copyright file="UnifiedAssemblyInfoEditCommand.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Linq;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.AssemblyInfoEditor;
using Microsoft.VisualStudio.Shell;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommonAssemblyInfoEditCommand :  ICommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("E1E48B05-DCEC-4759-AB5A-B4C642E84660");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonAssemblyInfoEditCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private CommonAssemblyInfoEditCommand(Package package)
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

        public CommonAssemblyInfoEditCommand()
        {
            
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommonAssemblyInfoEditCommand Instance
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
            Instance = new CommonAssemblyInfoEditCommand(package);
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
            var startProject = dte.GetStartupProject();
            var allProjects = dte.GetSolutionProjects().ToList();
            if (!allProjects.Any())
            {
                return;
            }
            new CommonAssemblyInfoForm(allProjects, startProject).ShowDialog();
        }
    }
}
