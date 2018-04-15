using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.Commands;

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
            BeginGroup = true;
            //SubMenus = new List<CommandMenu>
            //{
            //    new CommonAssemblyInfoEditMenu(),
            //    new AssemblyInfoEditMenu()
            //};
        }
    }
}