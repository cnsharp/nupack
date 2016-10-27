using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CnSharp.VisualStudio.Automan.AttributeAppenders;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var items = AttributeSnippetManager.Load();
            var xml = XmlSerializerHelper.GetXmlStringFromObject(items);
            Console.Write(xml);

            var obj = XmlSerializerHelper.LoadObjectFromXmlString<AttributeSnippet[]>(xml);
            Console.Write(obj[0].SubItems.Count);
        }

         [TestMethod]
        public void TestProjectAssemblyInfoUtil()
         {
             var file =
                 @"X:\workspace\intime\WMS\08_Code2013\00_Infrastructure\Intime.Framework\Intime.Framework.Caching\Intime.Framework.Caching\Properties\AssemblyInfo.cs";
             using (var sr = new StreamReader(file, Encoding.Default))
             {
                 var assemblyInfo = sr.ReadToEnd();
                 string fileVersion =
                Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyFileVersion\(""(?<content>.+)""\)").Groups["content"].Value;
                 string version = Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyVersion\(""(?<content>.+)""\)").Groups["content"].Value;
                 string productName =
                     Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyProduct\(""(?<content>.+)""\)").Groups["content"].Value;
                 string companyName =
                     Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyCompany\(""(?<content>.+)""\)").Groups["content"].Value;
                 string title =
                    Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyTitle\(""(?<content>.+)""\)").Groups["content"].Value;
                 string description =
                   Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyDescription\(""(?<content>.+)""\)").Groups["content"].Value;
                 string copyright =
                  Regex.Match(assemblyInfo, @"[^/]\[assembly:\s*AssemblyCopyright\(""(?<content>.+)""\)").Groups["content"].Value;
                 var info = new ProjectAssemblyInfo
                 {
                    
                     FileVersion = fileVersion,
                     Version = version,
                     ProductName = productName,
                     Company = companyName,
                     Title = title,
                     Description = description,
                     Copyright = copyright
                 };
                 var assemblyText = assemblyInfo;//.Replace("\r[", "\r\n[");
                 var matches = Regex.Matches(assemblyText, @"[^/]\[assembly:\s*AssemblyFileVersion\("".+""\)");
                 foreach (Match match in matches)
                 {
                     Console.WriteLine(match.Value);
                 }
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyFileVersion\("".+""\)",
                            string.Format("[assembly: AssemblyFileVersion(\"{0}\")", info.FileVersion));
              
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyVersion\("".+""\)",
                               string.Format("[assembly: AssemblyVersion(\"{0}\")", info.Version));
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyProduct\("".+""\)",
                               string.Format("[assembly: AssemblyProduct(\"{0}\")", info.ProductName));
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyCompany\("".*""\)",
                               string.Format("[assembly: AssemblyCompany(\"{0}\")", info.Company));
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyTitle\("".+""\)",
                               string.Format("[assembly: AssemblyTitle(\"{0}\")", info.Title));
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyDescription\("".+""\)",
                               string.Format("[assembly: AssemblyDescription(\"{0}\")", info.Description));
                 assemblyText = Regex.Replace(assemblyText, @"[^/]\[assembly:\s*AssemblyCopyright\("".+""\)",
                              string.Format("[assembly: AssemblyCopyright(\"{0}\")", info.Copyright));
                 Console.Write(assemblyText);
             }
         }
    }
}
