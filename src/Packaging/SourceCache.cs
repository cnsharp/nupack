using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.NuGet
{
    public class ProjectNuGetSourceCache
    {
        private readonly Project _project;
        private string _file;

        protected List<NuGetSource> SourceList = new List<NuGetSource>();

        public const string CacheFileName = "NuPack.Source.config";

        public ProjectNuGetSourceCache(Project project)
        {
            _project = project;

           _file = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), $"{Common.ProductName}\\{CacheFileName}");
            if (!File.Exists(_file))
                return;

            var sources = XmlSerializerHelper.LoadObjectFromXml<List<NuGetSource>>(_file);
            var list = Decrypt(sources);
            SourceList.AddRange(list);
        }

        public NuGetSource[] Sources => SourceList.ToArray();

        public bool Add(NuGetSource source)
        {
            if (SourceList.Any(m => m.Url == source.Url))
                return false;
            SourceList.Add(source);
            return true;
        }

        public void Remove(string url)
        {
            SourceList.RemoveAll(m => m.Url == url);
        }

        protected IEnumerable<NuGetSource> Encrypt(List<NuGetSource> sources)
        {
            //return sources.Select(source => new NuGetSource
            //{
            //    Url = source.Url,
            //    ApiKey = CnSharp.Security.DES.Encrypt3DES(source.ApiKey, Encoding.UTF8)
            //});
            return sources;
        }

        protected IEnumerable<NuGetSource> Decrypt(List<NuGetSource> sources)
        {
            //return sources.Select(source => new NuGetSource
            //{
            //    Url = source.Url,
            //    ApiKey = CnSharp.Security.DES.Decrypt3DES(source.ApiKey, Encoding.UTF8)
            //});
            return sources;
        }

        public void Save()
        {
            var list = Encrypt(SourceList).ToList();
            var xml = XmlSerializerHelper.GetXmlStringFromObject(list);
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var dir = Path.GetDirectoryName(_file);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            doc.Save(_file);
        }
    }

   
}
