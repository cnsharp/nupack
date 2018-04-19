using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Util;

namespace CnSharp.VisualStudio.NuPack.NuGets
{

    public static class NuSpecHelper
    {
        public static Tuple<Package,XmlDocument> Read(string nuspecFile)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(nuspecFile);
            var xml = xmlDoc.InnerXml;
            return new Tuple<Package, XmlDocument>(XmlSerializerHelper.LoadObjectFromXmlString<Package>(xml),xmlDoc);
        }

        public static void UpdateMetadata(this XmlDocument doc, Package.MetaData metadata)
        {
            var metadataNode = doc.SelectSingleNode("package/metadata");
            if (metadataNode == null)
                return;
            metadataNode.SetXmlNode("id", metadata.Id);
            metadataNode.SetXmlNode("title", metadata.Title);
            metadataNode.SetXmlNode("description", metadata.Description);
            metadataNode.SetXmlNode("owners", metadata.Owners);
            metadataNode.SetXmlNode("authors", metadata.Authors);
            metadataNode.SetXmlNode("version", metadata.Version);
            metadataNode.SetXmlNode("releaseNotes", metadata.ReleaseNotes);
            UpdateDependencies(doc,metadata);
        }

        private static void SetXmlNode(this XmlNode metadataNode,string key,string value)
        {
            var idNode = metadataNode.SelectSingleNode(key);
            if (idNode != null)
                idNode.InnerText = value;
        }


        public static void UpdateDependencies(this XmlDocument doc, Package.MetaData metadata)
        {
            var metadataNode = doc.SelectSingleNode("package/metadata");
            if (metadataNode == null)
                return;

            if (metadata.Dependencies != null)
            {
                var depNode = metadataNode.SelectSingleNode("dependencies");
                if (depNode == null)
                {
                    var node = doc.CreateElement("dependencies");
                    metadataNode.AppendChild(node);
                    depNode = node;
                }

                depNode.RemoveAll();
                var tempNode = doc.CreateElement("temp");
                tempNode.InnerXml = XmlSerializerHelper.GetXmlStringFromObject(metadata.Dependencies);
                depNode.InnerXml = tempNode.ChildNodes[0].InnerXml;
            }
        }

        public static void MergeDependency(this Package.MetaData metadata,
            IEnumerable<DependencyViewModel> dependencyViewModels)
        {
            var dependencies = dependencyViewModels.ToList();
            if (!dependencies.Any())
                return;

            //reset
            metadata.Dependencies = new Package.DependencySet();

            var dep = metadata.Dependencies;

            //remove exists
            dep?.Groups?.ForEach(g =>
            {
                g.Dependencies.RemoveAll(m => dependencies.Any(k => k.Id == m.Id));

            });
        
            dep?.Dependencies?.RemoveAll(m => dependencies.Any(k => k.Id == m.Id));


            //new target framework
            var frameworks = dependencies.Where(
                d =>
                    (dep.Groups == null || dep.Groups.All(g => g.TargetFramework != d.TargetFramework)))
                .Select(m => m.TargetFramework)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct().ToList();
            if (frameworks.Count > 0)
            {
                if (dep.Groups == null)
                    dep.Groups = new List<Package.DependencyGroup>();
                frameworks.ForEach(f => dep.Groups.Add(new Package.DependencyGroup
                {
                    TargetFramework = f
                }));
            }


            //append
            if (dep.Groups != null && dep.Groups.Count > 0)
            {
                //merge from packages.config
                dep.Groups.ForEach(g =>
                {
                    g.Dependencies.AddRange(
                        dependencies.Where(d => d.TargetFramework == g.TargetFramework && !d.DevelopmentDependency)
                            .Select(m => new Package.Dependency
                            {
                                Id = m.Id,
                                Version = m.Version
                            }));
                });

                //merge my dependencies without group
                if ( dep.Dependencies != null && dep.Dependencies.Count > 0)
                {
                    var g = dep.Groups.FirstOrDefault(m => m.TargetFramework == null);
                    if (g == null)
                    {
                        g = new Package.DependencyGroup();
                        dep.Groups.Add(g);
                    }
                    g.Dependencies = dep.Dependencies.ToList();
                    dep.Dependencies.Clear();
                }

              
            }
            else
            {
                if (dep.Dependencies == null)
                    dep.Dependencies = new List<Package.Dependency>();
                dep.Dependencies.AddRange(
                    dependencies.Where(p => string.IsNullOrWhiteSpace(p.TargetFramework) && !p.DevelopmentDependency)
                        .Select(m => new Package.Dependency
                        {
                            Id = m.Id,
                            Version = m.Version
                        }));
            }
            //clear empty groups
            dep?.Groups?.RemoveAll(g => !g.Dependencies.Any());
        }


        public static bool IsEmptyOrPlaceHolder(this string value)
        {
            return string.IsNullOrWhiteSpace(value) || value.StartsWith("$");
        }

        public static string ShortenVersionNumber(this string version)
        {
            while (version.EndsWith(".*"))
            {
                var num = version.TrimEnd('*').TrimEnd('.');
                version = num;
            }
            while (version.EndsWith(".0"))
            {
                var num = version.TrimEnd('0').TrimEnd('.');
                if (num.IndexOf(".", StringComparison.Ordinal) < 0)
                    break;
                version = num;
            }
            return version;
        }

        public static List<string> UpdateDependencyInSolution(string packageId, string newVersion)
        {
            var nuspecFiles = new List<string>();
            var projects = Host.Instance.DTE.GetSolutionProjects().ToList();
            projects.ForEach(p =>
            {
                var nuspecFile = Path.Combine(p.GetDirectory(), NuGetDomain.NuSpecFileName);
                if (File.Exists(nuspecFile))
                {
                    var pair = Read(nuspecFile);

                    if (pair.Item1.Metadata.Dependencies != null)
                    {
                        var found = false;
                        foreach (var g in pair.Item1.Metadata.Dependencies.Groups)
                        {
                            foreach (var d in g.Dependencies)
                            {
                                if (d.Id == packageId)
                                {
                                    d.Version = newVersion;
                                    found = true;
                                    break;
                                }
                            }
                        }
                        foreach (var d in pair.Item1.Metadata.Dependencies.Dependencies)
                        {
                            if (d.Id == packageId)
                            {
                                d.Version = newVersion;
                                found = true;
                                break;
                            }
                        }

                        if (found)
                        {
                            var metadata = new Package.MetaData
                            {
                                Dependencies = pair.Item1.Metadata.Dependencies
                            };
                            UpdateDependencies(pair.Item2, metadata);
                            Host.Instance?.SourceControl?.CheckOut(
                                Path.GetDirectoryName(Host.Instance.DTE.Solution.FullName), nuspecFile);
                            pair.Item2.Save(nuspecFile);
                            nuspecFiles.Add(nuspecFile);
                        }
                    }
                }
            });
            return nuspecFiles;
        }
    }
}