//------------------------------------------------------------------------------
// <copyright file="NuPackPackage.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace CnSharp.VisualStudio.NuPack
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#1110", "#1112", "1.0", IconResourceID = 1400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(NuPackPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.SolutionExists)]
    public sealed class NuPackPackage : Package
    {
        /// <summary>
        /// NuPackPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "cfa941e7-1101-459a-9777-496681f602d0";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNuSpecCommand"/> class.
        /// </summary>
        public NuPackPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.

            RedirectAssembly("VsSharp", new Version("1.2.0.0"), "31e1bdd79b8e5ae1");
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

          


            var dte = GetGlobalService(typeof(DTE)) as DTE2;
            Host.Instance.DTE = dte;

            AddNuSpecCommand.Initialize(this);
            NuGetDeployCommand.Initialize(this);
            AssemblyInfoEditCommand.Initialize(this);
            UnifiedAssemblyInfoEditCommand.Initialize(this);

            LoadCustomCommands();
        }

     
        public static void RedirectAssembly(string shortName, Version targetVersion, string publicKeyToken)
        {
            ResolveEventHandler handler = null;

            handler = (sender, args) => {
                // Use latest strong name & version when trying to load SDK assemblies
                var requestedAssembly = new AssemblyName(args.Name);
                if (requestedAssembly.Name != shortName)
                    return null;

                Debug.WriteLine("Redirecting assembly load of " + args.Name
                              + ",\tloaded by " + (args.RequestingAssembly == null ? "(unknown)" : args.RequestingAssembly.FullName));

                requestedAssembly.Version = targetVersion;
                requestedAssembly.SetPublicKeyToken(new AssemblyName(shortName+", PublicKeyToken=" + publicKeyToken).GetPublicKeyToken());
                requestedAssembly.CultureInfo = CultureInfo.InvariantCulture;

                AppDomain.CurrentDomain.AssemblyResolve -= handler;

                return Assembly.Load(requestedAssembly);
            };
            AppDomain.CurrentDomain.AssemblyResolve += handler;
        }

        private CommandConfig _config;
        private static Guid Id = new Guid("{738D452D-862C-4BB5-A035-CA8E07403138}");

        private  void LoadCustomCommands()
        {
            if (_config != null || Host.Plugins.Any(m => m.Id == Id))
                return;
            _config = new CommandConfig
            {
                Menus =
                {
                   new AssemblyInfoMenu()
                }
            };
            var plugin = new Plugin
            {
                Id = Id,
            CommandConfig = _config,
                Assembly = Assembly.GetExecutingAssembly(),
                Location =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""),
                //ResourceManager = new ResourceManager(config.ResourceManager, Assembly.GetExecutingAssembly())
            };


            var manager = new CommandManager(plugin);
            manager.Load();
        }

        #endregion
    }
}
