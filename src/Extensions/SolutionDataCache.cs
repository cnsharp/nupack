using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Extensions
{
    public class SolutionDataCache : ConcurrentDictionary<string,SolutionProperties>
    {
        private static SolutionDataCache instance;
        protected SolutionDataCache()
        {
           
        }

        public static SolutionDataCache Instance => instance ?? (instance = new SolutionDataCache());

        public SolutionProperties GetSolutionProperties(string solutionFile)
        {
            SolutionProperties sp;
            while (!TryGetValue(solutionFile,out sp))
            {
                System.Threading.Thread.Sleep(500);
            }
            return sp;
        }
    }

    public class SolutionProperties
    {
        public List<Project> Projects { get; set; }
        public List<Project> ClassicProjects { get; set; }
        public List<Project> SdkBasedProjects { get; set; }
        public bool HasClassicProjects => ClassicProjects?.Any() == true;
        public bool HasSdkBasedProjects => SdkBasedProjects?.Any() == true;
    }
}
