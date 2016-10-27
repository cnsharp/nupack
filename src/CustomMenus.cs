using System.Collections.Generic;
using CnSharp.VisualStudio.Extensions.Commands;

namespace CnSharp.VisualStudio.NuPack
{

    public class UnifiedAssemblyInfoEditMenu : CommandMenu
    {
        public UnifiedAssemblyInfoEditMenu()
        {
            Id = this.GetType().Name;
            Command = new UnifiedAssemblyInfoEditCommand();
            //AttachTo = "Solution";
            Text = "Unified Common Info...";
            Action = () => Command.Execute();
            //Position = -7;
            BeginGroup = true;
        }
    }

    public class AssemblyInfoEditMenu : CommandMenu
    {

        public AssemblyInfoEditMenu()
        {
            Id = this.GetType().Name;
            Command = new AssemblyInfoEditCommand();
            //AttachTo = "Solution";
            Text = "Edit One by One...";
            Action = () => Command.Execute();
            //Position = -7;
        }
    }

    public class AssemblyInfoMenu : CommandMenu
    {
        public AssemblyInfoMenu()
        {
            Id = this.GetType().Name;
            AttachTo = "Solution";
            Text = "Assembly Info...";
            Position = -7;
            SubMenus = new List<CommandMenu>
            {
                new UnifiedAssemblyInfoEditMenu(),
                new AssemblyInfoEditMenu()
            };
        }
    }
}
