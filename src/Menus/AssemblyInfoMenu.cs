using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.Commands;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Menus
{
    public class AssemblyInfoMenu : CommandMenu
    {
        public AssemblyInfoMenu()
        {
            Id = this.GetType().Name;
            AttachTo = "Solution";
            Text = "Assembly Info...";
            Position = -7;
            Command = new AssemblyInfoEditCommand();
            Action = () => Command.Execute();
            UnavailableState = ControlUnavailableState.Invisbile;
            DependentItems = DependentItems.SolutionProject;
            Action = () => Command.Execute();
            EnabledFunc = () =>
            {
                var sln = Host.Instance.DTE.Solution;
                if (sln == null) return false;
                return AnyClassicProjects(sln);
            };
            BeginGroup = true;
            //SubMenus = new List<CommandMenu>
            //{
            //    new CommonAssemblyInfoEditMenu(),
            //    new AssemblyInfoEditMenu()
            //};
        }

        bool AnyClassicProjects(Solution sln)
        {
            foreach(Project p in sln.Projects)
            {
                if (!string.IsNullOrWhiteSpace(p.FileName) && !p.IsSdkBased())
                    return true;
            }
            return false;
        }
    }
}