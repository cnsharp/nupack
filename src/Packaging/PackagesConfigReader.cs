using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace CnSharp.VisualStudio.NuPack.NuGets
{
    public class PackagesConfigReader
    {
        private readonly string _configFileName;
        private XmlDocument _doc;

        public PackagesConfigReader(string configFileName)
        {
            _configFileName = configFileName;
            _doc = new XmlDocument();
            _doc.Load(_configFileName);
        }

        public IEnumerable<DependencyViewModel> GetDependencies()
        {
            foreach (XmlNode node in _doc.ChildNodes[1].ChildNodes)
            {
                yield return new DependencyViewModel
                {
                    Id = node.Attributes["id"].Value,
                    Version = node.Attributes["version"].Value,
                    TargetFramework = node.Attributes["targetFramework"]?.Value,
                    DevelopmentDependency = node.Attributes["developmentDependency"]?.Value.ToLower() == "true"
                };
            }
            
        }

        public Package.DependencySet GetDependencySet()
        {
            var dependencies = GetDependencies().Where(m => !m.DevelopmentDependency).ToList();
            var set = new Package.DependencySet();
            var targets = dependencies.Select(r => r.TargetFramework).Distinct().ToList();

            if (targets.Any(t => !string.IsNullOrWhiteSpace(t)))
            {
                var groups = new List<Package.DependencyGroup>();
                foreach (var target in targets)
                {
                    var group = new Package.DependencyGroup
                    {
                        TargetFramework = string.IsNullOrWhiteSpace(target) ? null : target
                    };
                    Expression<Func<DependencyViewModel, bool>> exp = null;
                    if (string.IsNullOrWhiteSpace(target))
                        exp = r => string.IsNullOrWhiteSpace(r.TargetFramework);
                    else
                    {
                        exp = r => r.TargetFramework == target;
                    }
                    group.Dependencies =
                       dependencies
                            .Where(exp.Compile())
                            .Select(r => new Package.Dependency
                            {
                                Id = r.Id,
                                Version = r.Version
                            }).OrderBy(m => m.Id).ToList();
                    if (group.Dependencies.Count > 0)
                        groups.Add(group);
                }

                set.Groups = groups.OrderBy(m => m.TargetFramework).ToList();
            }
            else
            {
                set.Dependencies =
                       dependencies
                            .Select(r => new Package.Dependency
                            {
                                Id = r.Id,
                                Version = r.Version
                            }).OrderBy(m => m.Id).ToList();
            }

            return set;
        }
    }
}
