namespace CnSharp.VisualStudio.NuPack.NuGets
{
    public class DependencyViewModel
    {
        public string TargetFramework { get; set; }

        public string Id { get; set; }

        public string Version { get; set; }

        public bool DevelopmentDependency { get; set; }
    }
}