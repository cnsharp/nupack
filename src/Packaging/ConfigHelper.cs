using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Packaging
{
    public static class ConfigHelper
    {
        private static readonly string AppDataDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), Common.ProductName);
        private static readonly string NuGetConfigPath = Path.Combine(AppDataDir, NuGetConfig.FileName);

        public static NuGetConfig ReadNuGetConfig()
        {
            if(!File.Exists(NuGetConfigPath))
                return new NuGetConfig();
            return XmlSerializerHelper.LoadObjectFromXml<NuGetConfig>(NuGetConfigPath);
        }

        public static void Save(this NuGetConfig config)
        {
            var xml = XmlSerializerHelper.GetXmlStringFromObject(config);
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var dir = Path.GetDirectoryName(NuGetConfigPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            doc.Save(NuGetConfigPath);
        }

        public static ProjectNuPackConfig ReadNuPackConfig(this Project project)
        {
            var file = Path.Combine(AppDataDir, "\\" + project.UniqueName + ProjectNuPackConfig.Ext);
            if(!File.Exists(file))
                return new ProjectNuPackConfig(project);
            var config = XmlSerializerHelper.LoadObjectFromXml<ProjectNuPackConfig>(file);
            config.Project = project;
            return config;
        }

        public static void Save(this ProjectNuPackConfig config)
        {
            var file = Path.Combine(AppDataDir, "\\" + config.Project.UniqueName + ProjectNuPackConfig.Ext);
            var xml = XmlSerializerHelper.GetXmlStringFromObject(config);
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            doc.Save(file);
        }

        //private static string FindNuGetExePath()
        //{
        //    var pathes = Environment.GetEnvironmentVariables();
        //}
    }

    public class NuGetSource
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string UserName { get; set; }
    }

    public class NuGetConfig
    {
        public NuGetConfig()
        {
            Sources = new List<NuGetSource>();
        }
        public string NugetPath { get; set; }
        public List<NuGetSource> Sources { get; set; }
        public const string FileName = "nuget.nupack.config";

        public void AddOrUpdateSource(NuGetSource source)
        {
            Sources.RemoveAll(m => m.Url == source.Url);
            Sources.Add(source);
        }
    }

    public class ProjectNuPackConfig
    {
        [XmlIgnore]
        public  Project  Project { get; set; }

        public ProjectNuPackConfig()
        {
            
        }

        public ProjectNuPackConfig(Project project)
        {
            Project = project;
        }

        public const string Ext =  ".nupack.config";

        public string FileName => Project.UniqueName + Ext;

        public string PackageOutputDirectory { get; set; } = "bin\\NuGet\\";
    }
}
