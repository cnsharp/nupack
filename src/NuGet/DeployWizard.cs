using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AeroWizard;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using EnvDTE80;
using NuGet;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.NuGet
{
    public partial class DeployWizard : Form
    {
        private readonly ProjectAssemblyInfo _assemblyInfo;
        private readonly string _dir;
        private readonly NuGetConfig _nuGetConfig;
        private readonly string _nuspecFile;
        //private readonly Package _package;
        private readonly ManifestMetadata _metadata;
        private string _packageOldVersion;
        private readonly Project _project;
        private readonly ProjectNuPackConfig _projectConfig;
        private readonly string _releaseDir;
        private readonly XmlDocument _xmlDoc;
        private string _outputDir;


        public DeployWizard()
        {
            InitializeComponent();


            ActiveControl = txtAssemblyVersion;

            BindTextBoxEvents();

            _project = Host.Instance.Dte2.GetActiveProejct();
            _dir = _project.GetDirectory();
            _releaseDir = Path.Combine(_dir, "bin", "Release");
            _nuspecFile = Path.Combine(_dir, NuGetDomain.NuSpecFileName);


            _nuGetConfig = ConfigHelper.ReadNuGetConfig();
            _projectConfig = _project.ReadNuPackConfig();

            stepWizardControl.SelectedPageChanged += StepWizardControl_SelectedPageChanged;
            stepWizardControl.Finished += StepWizardControl_Finished;
            wizardPage1.Commit += WizardPageCommit;
            wizardPage2.Commit += WizardPageCommit;
        }

        private void WizardPageCommit(object sender, WizardPageConfirmEventArgs e)
        {
            var wp = sender as WizardPage;
            if (Validation.HasValidationErrors(wp.Controls))
            {
                e.Cancel = true;
            }
        }

        public DeployWizard(ProjectAssemblyInfo assemblyInfo, Package package, ManifestMetadata metadata,XmlDocument doc) : this()
        {
            _assemblyInfo = assemblyInfo;
            _metadata = metadata;
            _xmlDoc = doc;
        }

        private void StepWizardControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (stepWizardControl.SelectedPage == wizardPage1)
            {
                txtAssemblyVersion.Focus();
            }
            else if (stepWizardControl.SelectedPage == wizardPage2)
            {
                txtNugetPath.Focus();
            }
            else if (stepWizardControl.SelectedPage == wizardPage3)
            {
                sourceBox.Focus();
                textBoxSymbolServer.Enabled = chkSymbol.Checked;
                textBoxSymbolServer.Text = Common.SymbolServer;
            }
        }

        private void StepWizardControl_Finished(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }



        private void BindTextBoxEvents()
        {
            txtAssemblyVersion.TextChanged += TextBoxTextChanged;
            txtAssemblyVersion.TextChanged +=
                (sender, args) => { txtPackageVersion.Text = txtAssemblyVersion.Text.Trim().Trim('.').ShortenVersionNumber(); };
            txtPackageVersion.TextChanged += TextBoxTextChanged;
            txtNote.TextChanged += TextBoxTextChanged;

            MakeTextBoxRequired(textBoxId);
            MakeTextBoxRequired(textBoxTitle);
            MakeTextBoxRequired(textBoxAuthors);
            MakeTextBoxRequired(textBoxOwners);
            MakeTextBoxRequired(txtAssemblyVersion);
            MakeTextBoxRequired(txtPackageVersion);
            MakeTextBoxRequired(txtNote);
            MakeTextBoxRequired(txtNugetPath);
            MakeTextBoxRequired(txtOutputDir);
        }

        private void MakeTextBoxRequired(TextBox box)
        {
            box.Validating += TextBoxValidating;
            box.Validated += TextBoxValidated;
        }

        private void TxtOutputDir_Validating(object sender, CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            if (box.Text.Contains(":") && !Directory.Exists(box.Text.Trim()))
            {
                errorProvider.SetError(box, "Directory not found.");
                e.Cancel = true;
            }
        }

        private void TxtNugetPath_Validating(object sender, CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                errorProvider.SetError(box, "*");
                e.Cancel = true;
                return;
            }
            if (!File.Exists(box.Text.Trim()))
            {
                errorProvider.SetError(box, "File not found.");
                e.Cancel = true;
            }
        }

        private void TextBoxValidated(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            errorProvider.SetError(box, null);
        }

        private void TextBoxValidating(object sender, CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                errorProvider.SetError(box, "*");
                e.Cancel = true;
            }
        }

        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box != null && box.BackColor == SystemColors.Info)
                box.BackColor = SystemColors.Window;
        }

        private void DeployWizard_Load(object sender, EventArgs e)
        {
            SetBoxes();
        }


        private void SetBoxes()
        {
            if (_assemblyInfo != null)
            {
                txtAssemblyVersion.Text = _assemblyInfo.Version;
                //txtAssemblyVersion.Enabled = !_assemblyInfo.Version.Contains("*");
            }

            txtPackageVersion.Text = _metadata.Version.IsEmptyOrPlaceHolder() ? _assemblyInfo?.Version.Replace(".*", "") : _metadata.Version;
            txtNote.Text = XmlTextFormatter.Decode(_metadata.ReleaseNotes);
            textBoxId.Text = _metadata.Id;
            textBoxTitle.Text = _metadata.Title;
            textBoxAuthors.Text = _metadata.Authors;
            textBoxOwners.Text = _metadata.Owners;
            textBoxDescription.Text = _metadata.Description;

            foreach (var source in _nuGetConfig.Sources)
            {
                sourceBox.Items.Add(source.Url);
            }

            txtNugetPath.Text = _nuGetConfig.NugetPath;
            txtOutputDir.Text = _projectConfig.PackageOutputDirectory;

        }

        private void btnOpenNuGetExe_Click(object sender, EventArgs e)
        {
            if (openNugetExeDialog.ShowDialog() == DialogResult.OK)
            {
                txtNugetPath.Text = openNugetExeDialog.FileName;
            }
        }

        private void btnOpenOutputDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                txtOutputDir.Text = folderBrowserDialog.SelectedPath;
        }

        public void SaveAndBuild()
        {
            SaveAssemblyInfo();
            SaveNuSpec();
            if (!Build())
                return;
            EnsureOutputDir();
            Pack();
            //MovePackage();
           SyncVersionToDependency();
            SaveNuGetConfig();
           SaveProjectConfig();
        }


        private void SaveAssemblyInfo()
        {
            var newVersion = txtAssemblyVersion.Text.Trim();
            if (_assemblyInfo != null && newVersion != _assemblyInfo.Version)
            {
                _assemblyInfo.Version = _assemblyInfo.FileVersion = newVersion;
                AssemblyInfoUtil.Save(_assemblyInfo);
            }
        }

        private void SyncVersionToDependency()
        {
            if (_packageOldVersion == _metadata.Version)
                return;
            NuSpecHelper.UpdateDependencyInSolution(_metadata.Id.IsEmptyOrPlaceHolder() ? _assemblyInfo.Title : _metadata.Id, _metadata.Version);
        }

        private void SaveNuSpec()
        {
            var note = XmlTextFormatter.Encode(txtNote.Text);
            if (_metadata.Version != txtPackageVersion.Text.Trim())
            {
                _packageOldVersion = _metadata.Version;
                _metadata.Version = txtPackageVersion.Text.Trim();
            }

            _metadata.Id = textBoxId.Text.Trim();
            _metadata.Title = textBoxTitle.Text.Trim();
            _metadata.Authors = textBoxAuthors.Text.Trim();
            _metadata.Owners = textBoxOwners.Text.Trim();
            _metadata.Description = XmlTextFormatter.Encode(textBoxDescription.Text.Trim());
            _metadata.ReleaseNotes = note;

            var sc = Host.Instance.SourceControl;
            sc?.CheckOut(Path.GetDirectoryName(Host.Instance.DTE.Solution.FullName), _nuspecFile);

            //partly update //todo
            //_xmlDoc.UpdateMetadata(_metadata);
            //_xmlDoc.Save(_nuspecFile);
        }

        private bool Build()
        {
            var solution = (Solution2) Host.Instance.DTE.Solution;
            var solutionBuild = (SolutionBuild2) solution.SolutionBuild;
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
                @"""{0}"" pack ""{1}"" -Build -Version ""{2}"" -Properties  Configuration=Release -OutputDirectory ""{3}"" ", nugetExe,
                _project.FileName,_metadata.Version, _outputDir);

            if (chkForceEnglishOutput.Checked)
                script.Append(" -ForceEnglishOutput ");
            if(chkIncludeReferencedProjects.Checked)
                script.Append(" -IncludeReferencedProjects ");
            if(chkSymbol.Checked)
                script.Append(" -Symbols ");

            var url = sourceBox.Text.Trim();
            if (url.Length > 0)
            {
                script.AppendLine();
                if (checkBoxNugetLogin.Checked)
                {
                    script.AppendFormat(@"""{0}"" sources Add -Name ""{1}"" -Source ""{2}"" -Username ""{3}"" -Password ""{4}""", nugetExe, url, url, textBoxLogin.Text, txtKey.Text);
                    script.AppendFormat(@" || ""{0}"" sources Update -Name ""{1}"" -Source ""{2}"" -Username ""{3}"" -Password ""{4}""", nugetExe, url, url, textBoxLogin.Text, txtKey.Text);
                    script.AppendLine();
                }

                script.AppendFormat(@"""{0}"" push ""{1}"" -source {2} {3}", nugetExe, GetLastPackage(), url, txtKey.Text);
            }

            RunCmd(script.ToString());

            ShowPackages();

            if (chkSymbol.Checked && !string.IsNullOrWhiteSpace(textBoxSymbolServer.Text))
                PublishSymbolPackage();
        }


        string GetLastPackage()
        {
            var files = Directory.GetFiles(_outputDir, "*.nupkg");
            return files.Where(f => !f.EndsWith(".symbols.nupkg")).Select(m => new FileInfo(m)).OrderByDescending(f => f.CreationTime).FirstOrDefault().FullName;
        }

        string GetLastSymbolPackage()
        {
            var files = Directory.GetFiles(_outputDir, "*.symbols.nupkg");
            return files.Select(m => new FileInfo(m)).OrderByDescending(f => f.CreationTime).FirstOrDefault().FullName;
        }


        private void ShowPackages()
        {
            var outputDir = new DirectoryInfo(_outputDir);
            if (!outputDir.Exists)
                return;
            var files = outputDir.GetFiles("*.nupkg");
            if (chkOpenDir.Checked && files.Length > 0)
                Process.Start(_outputDir);
        }

        void PublishSymbolPackage()
        {
            var script = new StringBuilder();
            script.AppendLine();
            script.AppendFormat("nuget SetApiKey {0}", txtKey.Text);
            script.AppendLine();
            script.AppendFormat("nuget push {0} -source {1}",GetLastSymbolPackage(), textBoxSymbolServer.Text.Trim());
            RunCmd(script.ToString());
        }

        private void MovePackage()
        {
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
                fileCount++;
            }

            if (chkOpenDir.Checked && fileCount > 0)
                Process.Start(_outputDir);
        }

        private void EnsureOutputDir()
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
                    if (_outputDir.Length == 0)
                        _outputDir = _projectConfig.PackageOutputDirectory;
                    relativePath = true;
                }
            }
            if (relativePath)
            {
                _outputDir = _dir + "\\" + _outputDir + "\\";
                if (!Directory.Exists(_outputDir))
                    Directory.CreateDirectory(_outputDir);
            }
        }

        //cmd issues goes here:https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
        private void RunCmd(string script)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                //process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            Host.Instance.Dte2.OutputMessage(Common.ProductName, e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            Host.Instance.Dte2.OutputMessage(Common.ProductName, e.Data);
                        }
                    };

                    process.Start();
                    using (var writer = process.StandardInput)
                    {
                        if (writer.BaseStream.CanWrite)
                        {
                            writer.WriteLine(script);
                        }
                    }

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
            }
        }


        private void SaveNuGetConfig()
        {
            var url = sourceBox.Text.Trim();
            var nugetExePath = txtNugetPath.Text.Trim();
            _nuGetConfig.NugetPath = nugetExePath;
            if (url.Length > 0)
            {
                _nuGetConfig.AddOrUpdateSource(new NuGetSource {Url = url, ApiKey = chkRemember.Checked ?  txtKey.Text : null,
                    UserName = checkBoxNugetLogin.Checked ? textBoxLogin.Text : null});
            }
            _nuGetConfig.Save();
        }

        private void SaveProjectConfig()
        {
            if (txtOutputDir.Text == _projectConfig.PackageOutputDirectory)
                return;
            _projectConfig.PackageOutputDirectory = txtOutputDir.Text;
            _projectConfig.Save();
        }

        private void sourceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var url = sourceBox.Text.Trim();
            var source = _nuGetConfig.Sources.FirstOrDefault(m => m.Url == url);
            txtKey.Text = source?.ApiKey ?? string.Empty;
            chkRemember.Checked = txtKey.Text.Length > 0;
            textBoxLogin.Text = source?.UserName;
            checkBoxNugetLogin.Checked = textBoxLogin.Text.Length > 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (txtNote.Focused)
                {
                    txtNote.AppendText(Environment.NewLine);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void checkBoxNugetLogin_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;

            textBoxLogin.Visible = check.Checked;
            labelLogin.Visible = check.Checked;
        }

        private void btnOpenCommonAssemblyInfo_Click(object sender, EventArgs e)
        {
            var dr = openAssemblyInfoFileDialog.ShowDialog();
            if (dr != DialogResult.OK) return;
            var assemblyInfoFile = openAssemblyInfoFileDialog.FileName;
            try
            {
                var manager = AssemblyInfoFileManagerFactory.Get(_project);
                var info = manager.Read(assemblyInfoFile);
                if (string.IsNullOrWhiteSpace(textBoxAuthors.Text)) textBoxAuthors.Text = info.Company;
                if (string.IsNullOrWhiteSpace(textBoxOwners.Text)) textBoxOwners.Text = info.Company;
                if (string.IsNullOrWhiteSpace(textBoxDescription.Text)) textBoxDescription.Text = info.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}