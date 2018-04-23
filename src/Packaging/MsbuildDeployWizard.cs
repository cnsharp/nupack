/*
 * references
 * https://docs.microsoft.com/en-us/nuget/reference/msbuild-targets
 * https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AeroWizard;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using NuGet;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.Packaging
{
    public partial class MsbuildDeployWizard : Form
    {
        //private readonly ProjectAssemblyInfo _assemblyInfo;
        private readonly PackageProjectProperties _ppp;
        private readonly DirectoryBuildProps _directoryBuildProps;
        private readonly string _dir;
        private readonly NuGetConfig _nuGetConfig;
        //private readonly Package _package;
        private readonly ManifestMetadata _metadata;
        private readonly string _packageOldVersion;
        private readonly Project _project;
        private readonly ProjectNuPackConfig _projectConfig;
        private readonly string _releaseDir;
        private string _outputDir;
        private string _nuspecFile;
        private PackageMetadataControl _metadataControl;
        private NuGetDeployControl _deployControl;

        public MsbuildDeployWizard()
        {
            InitializeComponent();

            _metadataControl = new PackageMetadataControl();
            _metadataControl.Dock = DockStyle.Fill;
            _metadataControl.ErrorProvider = errorProvider;
            panelPackageInfo.Controls.Add(_metadataControl);
            ActiveControl = _metadataControl;

            _deployControl = new NuGetDeployControl();
            _deployControl.Dock = DockStyle.Fill;
            wizardPageDeploy.Controls.Add(_deployControl);

            _project = Host.Instance.Dte2.GetActiveProejct();
            _dir = _project.GetDirectory();
            _releaseDir = Path.Combine(_dir, "bin", "Release");
            _nuspecFile = _project.GetNuSpecFilePath();

            _nuGetConfig = ConfigHelper.ReadNuGetConfig();
            _projectConfig = _project.ReadNuPackConfig();

            BindTextBoxEvents();

            stepWizardControl.SelectedPageChanged += StepWizardControl_SelectedPageChanged;
            stepWizardControl.Finished += StepWizardControl_Finished;
            wizardPageMetadata.Commit += WizardPageCommit;
            wizardPageOptions.Commit += WizardPageCommit;
            chkSymbol.CheckedChanged += (sender, e) =>
            {
                if (_deployControl.ViewModel != null && string.IsNullOrWhiteSpace(_deployControl.ViewModel.SymbolServer))
                    _deployControl.ViewModel.SymbolServer = Common.SymbolServer;
            };
        }

        public MsbuildDeployWizard(ManifestMetadata metadata,  PackageProjectProperties ppp, DirectoryBuildProps directoryBuildProps) : this()
        {
            _metadata = metadata;
            _ppp = ppp;
            _directoryBuildProps = directoryBuildProps;
            _packageOldVersion = _metadata.Version;

            //if (_project.Properties.Item("Deterministic") != null &&
            //    Convert.ToBoolean(_project.Properties.Item("Deterministic").Value) == false)
            if(_metadata.Version.IsAutoVersion())
            {
                _metadata.Version = Version.Parse(_metadata.Version).GetWildCardVersionString();
                _ppp.Version = _ppp.AssemblyVersion = _ppp.FileVersion = _metadata.Version;
            }
        }

        private void WizardPageCommit(object sender, WizardPageConfirmEventArgs e)
        {
            var wp = sender as WizardPage;
            if (Validation.HasValidationErrors(wp.Controls))
            {
                e.Cancel = true;
            }
        }

        private void StepWizardControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (stepWizardControl.SelectedPage == wizardPageMetadata)
            {
                _metadataControl.Focus();
            }
            else if (stepWizardControl.SelectedPage == wizardPageOptions)
            {
                txtNugetPath.Focus();
            }
            else if (stepWizardControl.SelectedPage == wizardPageDeploy)
            {
                _deployControl.Focus();
                if (_deployControl.NuGetConfig == null)
                {
                    _deployControl.NuGetConfig = _nuGetConfig;
                    var deployVM = new NuGetDeployViewModel
                    {
                        SymbolServer = chkSymbol.Checked ? Common.SymbolServer : null
                    };
                    _deployControl.ViewModel = deployVM;
                }
            }
        }

        private void StepWizardControl_Finished(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }



        private void BindTextBoxEvents()
        {
            MakeTextBoxRequired(txtNugetPath);
            MakeTextBoxRequired(txtOutputDir);
            txtNugetPath.Validating += TxtNugetPath_Validating;
            txtOutputDir.Validating += TxtOutputDir_Validating;
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
            var ver = _ppp.AssemblyVersion;
            if (_metadata.Version.IsEmptyOrPlaceHolder())
                _metadata.Version = ver;//.Replace(".*", "");
            if (_metadata.Title.IsEmptyOrPlaceHolder())
                _metadata.Title = _metadata.Id;
            _metadataControl.ManifestMetadata = _metadata;
            _metadataControl.AssemblyInfo = new ProjectAssemblyInfo {Version = ver, FileVersion = _ppp.FileVersion};

         
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
            CheckWildCardVersion();
            SaveNuSpec();
            SaveProjectProperties();
            if (!Pack())
                return;
            EnsureOutputDir();
            MovePackages();
            Push();
            SyncVersionToDependency();
            SaveNuGetConfig();
            SaveProjectConfig();
        }

        void SaveProjectProperties()
        {
            _directoryBuildProps?.Save();
            var assemblyInfo = _metadataControl.AssemblyInfo;
            _ppp.Version = assemblyInfo.Version.EndsWith(".*") ? _metadata.Version : assemblyInfo.Version;
            _ppp.AssemblyVersion = assemblyInfo.Version.EndsWith(".*") ? _metadata.Version : assemblyInfo.Version;
            _ppp.FileVersion = assemblyInfo.FileVersion.EndsWith(".*") ? _metadata.Version : assemblyInfo.FileVersion;
            _metadata.SyncToPackageProjectProperties(_ppp);
            var skipProps = _directoryBuildProps?.GetValuedProperties()?.ToArray();
            _project.SavePackageProjectProperties(_ppp,skipProps);
        }

        private void SyncVersionToDependency()
        {
            if (_packageOldVersion == _metadata.Version)
                return;
            NuGetExtensions.UpdateDependencyInSolution(_metadata.Id, _metadata.Version);
        }

        void CheckWildCardVersion()
        {
            if (_metadata.Version.EndsWith(".*"))
            {
                _metadata.Version = Version.Parse(_metadata.Version.Replace(".*", "")).GetCurrentBuildVersionString();
                //_project.Properties.Item("Deterministic").Value = false;
            }
            else
            {
                //_project.Properties.Item("Deterministic").Value = true;
            }

        }

        private void SaveNuSpec()
        {
            
            if (SemanticVersion.TryParse(_metadata.Version, out var ver))
                _metadata.Version = ver.ToFullString();

            _project.UpdateNuspec(_metadata);
        }


        string GetMsbuildPath()
        {
            var dir = new DirectoryInfo(Application.StartupPath); //mostly like "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\"
            dir = dir.Parent.Parent;
            dir = new DirectoryInfo(Path.Combine(dir.FullName, "MSBuild")); //mostly like %ProgramFiles(x86)%\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\MSbuild.exe
            var files = dir.GetFiles("MSbuild.exe", SearchOption.AllDirectories);
            return files.FirstOrDefault()?.FullName;//todo:amd64
        }

        private bool Pack()
        {
            var script = new StringBuilder();
            script.AppendFormat(" \"{0}\" \"{1}\" /t:pack /p:Configuration=Release ", GetMsbuildPath(), _project.FileName);
            if (chkSymbol.Checked)
                script.Append(" /p:IncludeSymbols=true ");
            if (File.Exists(_nuspecFile))
                script.AppendFormat(" /p:NuspecFile=\"{0}\" ", _nuspecFile);
            CmdUtil.RunCmd(script.ToString());
            if (_metadata.Version.EndsWith(".*"))
            {
                var outputFileName = _project.Properties.Item("OutputFileName").Value.ToString();
                var outputFile = Path.Combine(_releaseDir, outputFileName);
                _metadata.Version = FileVersionInfo.GetVersionInfo(outputFile).FileVersion;
            }
            var file = $"{_releaseDir}\\{_metadata.Id}.{_metadata.Version}.nupkg";
            return File.Exists(file);
        }

        private void Push()
        {
            var nugetExe = txtNugetPath.Text;
            var script = new StringBuilder();
            var deployVM = _deployControl.ViewModel;
            if (deployVM.NuGetServer.Length > 0)
            {
                script.AppendLine();
                if (!string.IsNullOrWhiteSpace(deployVM.V2Login))
                {
                    script.AppendFormat(@"""{0}"" sources Add -Name ""{1}"" -Source ""{2}"" -Username ""{3}"" -Password ""{4}""", nugetExe, deployVM.NuGetServer, deployVM.NuGetServer, deployVM.V2Login, deployVM.ApiKey);
                    script.AppendFormat(@" || ""{0}"" sources Update -Name ""{1}"" -Source ""{2}"" -Username ""{3}"" -Password ""{4}""", nugetExe, deployVM.NuGetServer, deployVM.NuGetServer, deployVM.V2Login, deployVM.ApiKey);
                    script.AppendLine();
                }

                script.AppendFormat("\"{0}\" push \"{1}{4}.{5}.nupkg\" -source {2} {3}", nugetExe, _outputDir, deployVM.NuGetServer, deployVM.ApiKey,
                    _metadata.Id, _metadata.Version);
            }

            if (chkSymbol.Checked && !string.IsNullOrWhiteSpace(deployVM.SymbolServer))
            {
                script.AppendLine();
                script.AppendFormat("nuget SetApiKey {0}", deployVM.ApiKey);
                script.AppendLine();
                script.AppendFormat("nuget push \"{0}{1}.{2}.symbols.nupkg\" -source {3}", _outputDir, _metadata.Id, _metadata.Version, deployVM.SymbolServer);
            }

            CmdUtil.RunCmd(script.ToString());

            ShowPackages();
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

        private void MovePackages()
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

    


        private void SaveNuGetConfig()
        {
            var deployVM = _deployControl.ViewModel;
            var nugetExePath = txtNugetPath.Text.Trim();
            _nuGetConfig.NugetPath = nugetExePath;
            if (!string.IsNullOrWhiteSpace(deployVM.NuGetServer))
            {
                _nuGetConfig.AddOrUpdateSource(new NuGetSource {
                    Url = deployVM.NuGetServer,
                    ApiKey = deployVM.RememberKey ? deployVM.ApiKey : null,
                    UserName = deployVM.V2Login});
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

      
       
    }
}