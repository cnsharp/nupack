using System.IO;
using System.Linq;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.Commands;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Menus
{
    public class AddDirectoryBuildPropsMenu : CommandMenu
    {
        public AddDirectoryBuildPropsMenu()
        {
            Id = this.GetType().Name;
            AttachTo = "Solution";
            Text = "Add Directory.Build.props";
            Position = -8;
            Command = new AddDirectoryBuildPropsCommand();
            UnavailableState = ControlUnavailableState.Invisbile;
            DependentItems = DependentItems.SolutionProject;
            Action = () => Command.Execute();
            EnabledFunc = () =>
            {
                var sln = Host.Instance.DTE.Solution;
                if (sln == null) return false;
                return sln.Projects.Cast<Project>().Any(p => !string.IsNullOrWhiteSpace(p.FileName) && p.IsSdkBased()) && !File.Exists(sln.GetDirectoryBuildPropsPath());
            };
        }
    }
}