using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace CnSharp.VisualStudio.NuPack.NuGet
{
    [XmlRoot("package")]
    public class Package
    {
        public Package()
        {
            Metadata = new MetaData();
        }

        [XmlArray("files"), XmlArrayItem("file")]
        public File[] Files { get; set; }

        [XmlElement("metadata")]
        public MetaData Metadata { get; set; }


        public class MetaData
        {
            [XmlElement("id")]
            public string Id { get; set; }


            [XmlElement("version")]
            public string Version { get; set; }


            [XmlElement("authors")]
            public string Authors { get; set; }

            [XmlElement("owners")]
            public string Owners { get; set; }

            [XmlElement("copyright")]
            public string Copyright { get; set; }

            [XmlElement("description")]
            public string Description { get; set; }


            [XmlElement("releaseNotes")]
            public string ReleaseNotes { get; set; }

            [XmlElement("iconUrl")]
            public string IconUrl { get; set; }

            [XmlElement("projectUrl")]
            public string ProjectUrl { get; set; }

            [XmlElement("licenseUrl")]
            public string LicenseUrl { get; set; }

            [XmlElement("requireLicenseAcceptance")]
            public bool RequireLicenseAcceptance { get; set; }

            [XmlElement("title")]
            public string Title { get; set; }


            [XmlElement("tags")]
            public string Tags { get; set; }

            [XmlElement("dependencies")]
            public DependencySet Dependencies { get; set; }

            [XmlElement("references")]
            public ReferenceSet References { get; set; }

        }

        public class File
        {
            [XmlElement("src")]
            public string Src { get; set; }

            [XmlAttribute("target")]
            public string Target { get; set; }
        }

        public class DependencyGroup
        {
            public DependencyGroup()
            {
                Dependencies = new List<Dependency>();
            }
            [XmlAttribute("targetFramework")]
            public string TargetFramework { get; set; }

            [XmlElement("dependency")]
            public List<Dependency> Dependencies { get; set; }
        }

        public class Dependency
        {
            [XmlAttribute("id")]
            public string Id { get; set; }

            [XmlAttribute("version")]
            public string Version { get; set; }

            [XmlAttribute("developmentDependency")]
            public bool DevelopmentDependency { get; set; }
        }


        public class DependencySet
        {
            [XmlElement("group")]
            public List<DependencyGroup> Groups { get; set; }

            [XmlElement("dependency")]
            public List<Dependency> Dependencies { get; set; }

            public bool Any()
            {
                return (Groups != null && Groups.Count > 0) || (Dependencies != null && Dependencies.Count > 0);
            }
        }


        public class ReferenceSet
        {
            [XmlElement("group")]
            public ReferenceGroup[] Groups { get; set; }

            [XmlElement("reference")]
            public ReferenceFile[] ReferenceFiles { get; set; }

            public class ReferenceGroup
            {
                [XmlAttribute("targetFramework")]
                public string TargetFramework { get; set; }

                [XmlElement("reference")]
                public ReferenceFile[] ReferenceFiles { get; set; }
            }

            public class ReferenceFile
            {
                [XmlAttribute("file")]
                public string File { get; set; }
            }
        }
    }

    public class DependencyViewModel
    {
        public string TargetFramework { get; set; }

        public string Id { get; set; }

        public string Version { get; set; }
    }
}