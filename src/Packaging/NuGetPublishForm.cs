using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.Xml;
using EnvDTE;
using EnvDTE80;

namespace CnSharp.VisualStudio.NuPack.NuGet
{
    public partial class NuGetPublishForm : Form
    {
        private readonly Project _project;
        private ProjectNuGetSourceCache _cache;
        private Package _package;
        private string _dir;
        private string _nuspecFile;
        private ProjectAssemblyInfo _assemblyInfo;

        public NuGetPublishForm()
        {
            InitializeComponent();

            _project = Host.Instance.Dte2.GetActiveProejct();
            _dir = _project.GetDirectory();
            _assemblyInfo = _project.GetProjectAssemblyInfo();
        }

        private void NuGetPublishForm_Load(object sender, System.EventArgs e)
        {
            
            _nuspecFile = Path.Combine(_dir, NuGetDomain.NuSpecFileName);
            if (!File.Exists(_nuspecFile))
            {
                return;
            }

            _package = XmlSerializerHelper.LoadObjectFromXml<Package>(_nuspecFile);
            if (_assemblyInfo != null)
            {
                txtVersion.Text = _assemblyInfo.Version;
                txtVersion.Enabled = !_assemblyInfo.Version.Contains("*");
            }
            txtNote.Text = _package.Metadata.ReleaseNotes;

            _cache = new ProjectNuGetSourceCache(_project);
            foreach (var source in _cache.Sources)
            {
                sourceBox.Items.Add(source.Url);
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
          
            if (txtNote.Text.Trim().Length == 0)
                return;
            SaveAssemblyInfo();
            SaveNuSpec();
            if (!Build())
                return;
            Pack();
        }

        private void SaveAssemblyInfo()
        {
            var newVersion = txtVersion.Text.Trim();
            if (_assemblyInfo == null || !txtVersion.Enabled || newVersion == _assemblyInfo.Version)
                return;
            _assemblyInfo.Version = _assemblyInfo.FileVersion = newVersion;

            _project.ModifyAssemblyInfo(_assemblyInfo);
        }

        private void SaveNuSpec()
        {
            if (_package != null &&_package.Metadata != null && _package.Metadata.ReleaseNotes == txtNote.Text)
                return;

            _package.Metadata.ReleaseNotes = txtNote.Text;

            var sc = Host.Instance.SourceControl;
            if (sc != null)
                sc.CheckOut(Path.GetDirectoryName(Host.Instance.DTE.Solution.FullName), _nuspecFile);

            var xml = XmlSerializerHelper.GetXmlStringFromObject(_package);
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.Save(_nuspecFile);
        }

        private bool Build()
        {
            var solution = (Solution2)Host.Instance.DTE.Solution;
            var solutionBuild = (SolutionBuild2)solution.SolutionBuild;
            solutionBuild.Build(true);
            if (solutionBuild.LastBuildInfo != 0)
            {
                return false;
            }
            return true;
        }

        private void Pack()
        {
            
            var nugetDir = Path.Combine(_project.GetDirectory(), "\\bin\\NuGet\\");
            if (!Directory.Exists(nugetDir))
                Directory.CreateDirectory(nugetDir);

            var script = new StringBuilder();
            script.AppendFormat(
                "nuget pack \"{0}\" -Properties Configuration=Nuget -OutputDirectory \"{1}\" ", _dir,nugetDir);
            var url = sourceBox.Text.Trim();
            if (url.Length > 0)
            {
                script.AppendLine();
                script.AppendFormat("nuget push \"{0}*.nupkg\" -source {1} {2}", nugetDir, url, txtKey.Text);
            }

            RunCmd(script.ToString());
        }

        private static void RunCmd(string script)
        {
            var process = new System.Diagnostics.Process();
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false
            };
            process.StartInfo = info;
            process.Start();
            using (StreamWriter writer = process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    writer.WriteLine(script);
                }
            }
            process.WaitForExit();
        }

        private void sourceBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var source = _cache.Sources.FirstOrDefault(m => m.Url == sourceBox.Text.Trim());
            if (source != null)
            {
                txtKey.Text = source.ApiKey;
            }
        }
    }
}
