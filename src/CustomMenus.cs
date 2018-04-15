using System.Collections.Generic;
using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.Commands;

namespace CnSharp.VisualStudio.NuPack
{

    public class CommonAssemblyInfoEditMenu : CommandMenu
    {
        public CommonAssemblyInfoEditMenu()
        {
            Id = this.GetType().Name;
            Command = new CommonAssemblyInfoEditCommand();
            //AttachTo = "Solution";
            Text = "Common Info...";
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
}
