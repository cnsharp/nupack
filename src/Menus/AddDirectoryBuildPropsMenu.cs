using CnSharp.VisualStudio.Extensions.Commands;
using CnSharp.VisualStudio.NuPack.Commands;

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
            Action = () => Command.Execute();
        }
    }
}