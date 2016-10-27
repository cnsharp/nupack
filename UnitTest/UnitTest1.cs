using System;
using System.Collections.Generic;
using CnSharp.VisualStudio.NuPack.NuGet;
using CnSharp.VisualStudio.NuPack.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var pack = new Package();
            //pack.Metadata = new Package.MetaData
            //{
            //    Dependencies = new Package.DependencySet
            //    {
            //        Dependencies = new List<Package.DependencySet.Dependency>
            //        {
            //            new Package.DependencySet.Dependency() {Id = "log4net", Version = "1.0"}
            //        }
            //    }
            //};
           

            //var xml = XmlSerializerHelper.GetXmlStringFromObject(pack);
            //Console.Write(xml);
        }

        [TestMethod]
        public void TestRead()
        {
            var xml = @"<?xml version=""1.0""?>
<package>
  <metadata>
    <id>CnSharp.VisualStudio.TFS</id>
    <version>1.1</version>
    <title>$title$</title>
    <authors>$author$</authors>
    <owners>$author$</owners>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>$description$</description>
    <releaseNotes>initial version</releaseNotes>
    <copyright>$copyright$</copyright>
    <iconUrl>https://secure.gravatar.com/avatar/dff08c1ffd5fe20a93c2a606aa88e0d5?s=32&amp;r=g&amp;d=retro</iconUrl>
    <projectUrl>http://vssharp.codeplex.com/</projectUrl>
    <dependencies>
   <dependency id=""VsSharp"" version=""1.1"" />
</dependencies>
  </metadata>
</package>";
            var p = XmlSerializerHelper.LoadObjectFromXmlString<Package>(xml);
            Assert.IsNotNull(p.Metadata.Dependencies);
        }
    }
}
