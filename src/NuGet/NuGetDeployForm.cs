using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using EnvDTE;
using EnvDTE80;
using Host = CnSharp.VisualStudio.Extensions.Host;
using CnSharp.VisualStudio.NuPack.Util;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.NuGet
{
    public partial class NuGetDeployForm : Form
    {
        private readonly Project _project;
        private readonly NuGetConfig _nuGetConfig;
        private readonly ProjectNuPackConfig _projectConfig;
        private readonly XmlDocument _xmlDoc;
        private readonly Package _package;
        private readonly string _dir;
        private readonly string _nuspecFile;
        private readonly string _releaseDir;
        private  string _outputDir;
        private readonly ProjectAssemblyInfo _assemblyInfo;

        public NuGetDeployForm()
        {
            InitializeComponent();

            BindTextBoxEvents();

            _project = Host.Instance.Dte2.GetActiveProejct();
            _dir = _project.GetDirectory();
            _releaseDir = Path.Combine(_dir, "bin", "Release");
            //_outputDir = Path.Combine(_dir, "bin", "NuGet");
            //if (!Directory.Exists(_outputDir))
            //    Directory.CreateDirectory(_outputDir);
            _nuspecFile = Path.Combine(_dir, NuGetDomain.NuSpecFileName);
            if (!File.Exists(_nuspecFile))
            {
                MessageBox.Show($"nuspec file {_nuspecFile} not found.");
                Close();
                return;
            }

            _assemblyInfo = _project.GetProjectAssemblyInfo();
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(_nuspecFile);
            var xml = _xmlDoc.InnerXml;
            _package = XmlSerializerHelper.LoadObjectFromXmlString<Package>(xml);
            _nuGetConfig = ConfigHelper.ReadNuGetConfig();
            _projectConfig = _project.ReadNuPackConfig();
            SetBoxes();
            MergePackagesConfig();
            BindDependencies();
        }

        public IServiceProvider ServiceProvider { get; set; }

        private void BindTextBoxEvents()
        {
            txtId.TextChanged += TextBoxTextChanged;
            txtAuthor.TextChanged += TextBoxTextChanged;
            txtDesc.TextChanged += TextBoxTextChanged;
            txtAssemblyVersion.TextChanged += TextBoxTextChanged;
            txtNote.TextChanged += TextBoxTextChanged;
        }

        private void SetBoxes()
        {
           
            if (_assemblyInfo != null)
            {
                txtAssemblyVersion.Text = _assemblyInfo.Version;
                txtAssemblyVersion.Enabled = !_assemblyInfo.Version.Contains("*");
                txtPackageVersion.Text = _assemblyInfo.Version.Replace(".*", "");
                txtAuthor.Text = _assemblyInfo.Company;
                txtDesc.Text = _assemblyInfo.Description;
                txtId.Text = _package.Metadata.Id == "$id$" ?  _assemblyInfo.Title : _package.Metadata.Id;
                txtCopyright.Text = _assemblyInfo.Copyright;
            }
            txtNote.Text = _package.Metadata.ReleaseNotes;


            foreach (var source in _nuGetConfig.Sources)
            {
                sourceBox.Items.Add(source.Url);
            }

            txtNugetPath.Text = _nuGetConfig.NugetPath;

            txtOutputDir.Text = _projectConfig.PackageOutputDirectory;
        }


        private void MergePackagesConfig()
        {
            var file = Path.Combine(Path.GetDirectoryName(_project.FileName), "packages.config");
            if (!File.Exists(file))
                return;
            var reader = new PackagesConfigReader(file);
            var packages = reader.GetDependencies().ToList();
            _package.Metadata.MergeDependency(packages);
        }


        private void BindDependencies()
        {
            dependencyGrid.Rows.Clear();
            var dep = _package.Metadata.Dependencies;
            if (dep == null)
                return;
            if (dep.Dependencies != null)
            {
                foreach (var dependency in dep.Dependencies)
                {
                    dependencyGrid.Rows.Add(dependency.Id, dependency.Version);
                }
            }
            if (dep.Groups != null)
            {
                foreach (var @group in dep.Groups)
                {
                    if (@group.Dependencies == null)
                        continue;
                    foreach (var dependency in @group.Dependencies)
                    {
                        dependencyGrid.Rows.Add(dependency.Id, dependency.Version,@group.TargetFramework);
                    }
                }
            }
        }

        private Package.DependencySet GetDependencySet()
        {
            var set = new Package.DependencySet();
            var targets =
                dependencyGrid.Rows.Cast<DataGridViewRow>()
                    .Select(r => r.Cells[2].Value?.ToString().Trim() ?? "").Distinct();

            if (targets.Any(t => !string.IsNullOrWhiteSpace(t)))
            {
                var groups = new List<Package.DependencyGroup>();
                foreach (var target in targets)
                {
                    var group = new Package.DependencyGroup
                    {
                        TargetFramework = string.IsNullOrWhiteSpace(target) ? null : target
                    };
                    Expression<Func<DataGridViewRow, bool>> exp = null;
                    if (string.IsNullOrWhiteSpace(target))
                        exp = (r => (r.Cells[0].Value != null && r.Cells[1].Value != null ) && (r.Cells[2].Value == null || string.IsNullOrWhiteSpace(r.Cells[2].Value.ToString())));
                    else
                    {
                        exp = (r => (r.Cells[0].Value != null && r.Cells[1].Value != null) && r.Cells[2].Value != null && r.Cells[2].Value.ToString().Trim() == target);
                    }
                    group.Dependencies =
                        dependencyGrid.Rows.Cast<DataGridViewRow>()
                            .Where(exp.Compile())
                            .Select(r => new Package.Dependency
                            {
                                Id = r.Cells[0].Value.ToString().Trim(),
                                Version = r.Cells[1].Value.ToString().Trim()
                            }).OrderBy(m => m.Id).ToList();
                    if(group.Dependencies.Count > 0)
                        groups.Add(group);
                }

                set.Groups = groups.OrderBy(m => m.TargetFramework).ToList();
            }
            else
            {
                set.Dependencies = 
                        dependencyGrid.Rows.Cast<DataGridViewRow>().Where(r => (r.Cells[0].Value != null && r.Cells[1].Value != null))
                            .Select(r => new Package.Dependency
                            {
                                Id = r.Cells[0].Value.ToString().Trim(),
                                Version = r.Cells[1].Value.ToString().Trim()
                            }).OrderBy(m => m.Id).ToList();
            }

            return set;
        }

        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box != null && box.BackColor == SystemColors.Info)
                box.BackColor = SystemColors.Window;
        }

        private void NuGetPublishForm_Load(object sender, System.EventArgs e)
        {
            
           
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (txtId.Text.Trim().Length == 0)
            {
                MarkTextBox(txtId);
                return;
            }
            if (txtAuthor.Text.Trim().Length == 0)
            {
                MarkTextBox(txtAuthor);
                return;
            }
            if (txtAssemblyVersion.Text.Trim().Length == 0)
            {
                MarkTextBox(txtAssemblyVersion);
                return;
            }
            if (txtPackageVersion.Text.Trim().Length == 0)
            {
                MarkTextBox(txtPackageVersion);
                return;
            }
            if (txtDesc.Text.Trim().Length == 0)
            {
                MarkTextBox(txtDesc);
                return;
            }

            if (txtNote.Text.Trim().Length == 0)
            {
                MarkTextBox(txtNote);
                return;
            }

            if (txtNugetPath.Text.Trim().Length == 0 || !File.Exists(txtNugetPath.Text))
            {
                if (!FindNuGetExe())
                    return;
            }
            //if (sourceBox.Text.Trim().Length == 0)
            //{
            //    MarkComboBox(sourceBox);
            //    return;
            //}
           
            DialogResult = DialogResult.OK;
        }

        public void SaveAndBuild()
        {
            SaveAssemblyInfo();
            SaveNuSpec();
            //MovePackage();
            if (!Build())
                return;
            Pack();
            MovePackage();
            SaveNuGetConfig();
            SaveProjectConfig();
        }

        private void MarkTextBox(TextBox box)
        {
            box.BackColor = SystemColors.Info;
            box.Focus();
        }

        private void MarkComboBox(ComboBox box)
        {
            box.BackColor = SystemColors.Info;
            box.Focus();
        }

        private void SetTextBoxBorder(TextBox box,Color color,Graphics g)
        {
            box.BorderStyle = BorderStyle.None;
            Pen p = new Pen(color);
            int variance = 3;
            g.DrawRectangle(p, new Rectangle(box.Location.X - variance, box.Location.Y - variance, box.Width + variance, box.Height + variance));
        }

        private void SaveAssemblyInfo()
        {
            var newVersion = txtAssemblyVersion.Text.Trim();
            if (_assemblyInfo != null )//&& txtAssemblyVersion.Enabled && newVersion != _assemblyInfo.Version)
            {
                _assemblyInfo.Version = _assemblyInfo.FileVersion = newVersion;
                _assemblyInfo.Company =  txtAuthor.Text.Trim();
                _assemblyInfo.Description = txtDesc.Text;
                _assemblyInfo.Copyright = txtCopyright.Text;
                //_assemblyInfo.Title = txtId.Text.Trim();
            }
            _project.ModifyAssemblyInfo(_assemblyInfo);
        }

        private void SaveNuSpec()
        {
            if(txtId.Text.Trim() != _assemblyInfo.Title)
                 _package.Metadata.Id = txtId.Text.Trim();
            if(_package.Metadata.Version != txtPackageVersion.Text.Trim())
                _package.Metadata.Version = txtPackageVersion.Text.Trim();
            if(_package.Metadata.ReleaseNotes != txtNote.Text)
                _package.Metadata.ReleaseNotes = txtNote.Text;
            if (_package.Metadata.Copyright != txtCopyright.Text)
                _package.Metadata.Copyright = txtCopyright.Text;
            if (_package.Metadata.Tags != txtTags.Text)
                _package.Metadata.Tags = txtTags.Text;
            _package.Metadata.Dependencies = GetDependencySet();

            var sc = Host.Instance.SourceControl;
            sc?.CheckOut(Path.GetDirectoryName(Host.Instance.DTE.Solution.FullName), _nuspecFile);

            //partly update
            _xmlDoc.UpdateMetadata(_package.Metadata);
            _xmlDoc.Save(_nuspecFile);
        }

        private bool Build()
        {
            var solution = (Solution2)Host.Instance.DTE.Solution;
            var solutionBuild = (SolutionBuild2)solution.SolutionBuild;
            solutionBuild.SolutionConfigurations.Item("Release").Activate();

            solutionBuild.Build(true);
            if (solutionBuild.LastBuildInfo != 0)
            {
                return false;
            }
            return true;
        }

        private void Pack()
        {
            var nugetExe = txtNugetPath.Text;
            var script = new StringBuilder();
            script.AppendFormat(
                @"{0} pack ""{1}"" -Properties Configuration=Release -OutputDirectory ""{2}"" ", nugetExe, _project.FileName, _releaseDir);
            var url = sourceBox.Text.Trim();
            if (url.Length > 0)
            {
                script.AppendLine();
                var dir = _releaseDir;
                if (!dir.EndsWith("\\"))
                    dir += "\\";
                script.AppendFormat(@"{0} push ""{1}*.nupkg"" -source {2} {3}", nugetExe,dir, url, txtKey.Text);
            }

            RunCmd(script.ToString());
        }

        private void MovePackage()
        {
            _outputDir = txtOutputDir.Text.Trim().Replace("/", "\\");
            var relativePath = false;
            if (_outputDir.Length == 0 || !Directory.Exists(_outputDir))
            {
                if (_outputDir.Contains(":\\"))
                {
                    try
                    {
                        Directory.CreateDirectory(_outputDir);
                    }
                    catch
                    {
                        _outputDir = _projectConfig.PackageOutputDirectory;
                        relativePath = true;
                    }
                }
                else
                {
                    if(_outputDir.Length == 0)
                        _outputDir = _projectConfig.PackageOutputDirectory;
                    relativePath = true;
                }
            }
            if (relativePath)
            {
                _outputDir = _dir + "\\" + _outputDir+"\\";
                if(!Directory.Exists(_outputDir))
                    Directory.CreateDirectory(_outputDir);
            }
            var releaseDir = new DirectoryInfo(_releaseDir);
            if (!releaseDir.Exists)
                return;
            var files = releaseDir.GetFiles("*.nupkg");
            var fileCount = 0;
            foreach (var file in files)
            {
                var dest = Path.Combine(_outputDir, file.Name);
                if (File.Exists(dest))
                {
                    File.Delete(dest);
                }
                file.MoveTo(dest);
                fileCount ++;
            }

            if (chkOpenDir.Checked && fileCount > 0)
                Process.Start(_outputDir);
        }

        private static void RunCmd(string script)
        {
            var process = new Process();
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
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
            var sr = process.StandardOutput;
            var msg = sr.ReadToEnd();
          
            var srError = process.StandardError;
          msg +=  srError.ReadToEnd();
            Host.Instance.Dte2.OutputMessage("NuPack", msg);
        }


        private void SaveNuGetConfig()
        {
            var url = sourceBox.Text.Trim();
            var nugetExePath = txtNugetPath.Text.Trim();
            if (url.Length == 0 && _nuGetConfig.NugetPath == nugetExePath)
                return;
            _nuGetConfig.NugetPath = nugetExePath;
            if(url.Length > 0)
                _nuGetConfig.AddSource(new NuGetSource {Url = url, ApiKey = txtKey.Text});
            _nuGetConfig.Save();
        }

        void SaveProjectConfig()
        {
            if (txtOutputDir.Text == _projectConfig.PackageOutputDirectory)
                return;
            _projectConfig.PackageOutputDirectory = txtOutputDir.Text;
            _projectConfig.Save();
        }
      

        private void sourceBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var source = _nuGetConfig.Sources.FirstOrDefault(m => m.Url == sourceBox.Text.Trim());
            if (source != null)
            {
                txtKey.Text = source.ApiKey;
            }
        }

        private void dependencyGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex < 2 && (string.IsNullOrWhiteSpace(e.FormattedValue?.ToString())))
            {
                dependencyGrid.Rows[e.RowIndex].ErrorText = "*";
                e.Cancel = true;
                dependencyGrid.EndEdit();
            }
        }

        private void dependencyGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dependencyGrid.Rows[e.RowIndex].ErrorText = null;
        }

        private void btnOpenNuGetExe_Click(object sender, EventArgs e)
        {
            FindNuGetExe();
        }

        bool FindNuGetExe()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtNugetPath.Text = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        private void btnOpenOutputDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                txtOutputDir.Text = folderBrowserDialog.SelectedPath;
        }

        private void txtAssemblyVersion_TextChanged(object sender, EventArgs e)
        {
            txtPackageVersion.Text = txtAssemblyVersion.Text.Trim();
        }
    }
}
