using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AeroWizard;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using EnvDTE80;
using NuGet;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.NuGets
{
    public partial class DeployWizard : Form
    {
        private readonly ProjectAssemblyInfo _assemblyInfo;
        private readonly PackageProjectProperties _ppp;
        private readonly string _dir;
        private readonly NuGetConfig _nuGetConfig;
        //private readonly Package _package;
        private readonly ManifestMetadata _metadata;
        private readonly string _packageOldVersion;
        private readonly Project _project;
        private readonly ProjectNuPackConfig _projectConfig;
        private readonly string _releaseDir;
        private string _outputDir;
        private PackageMetadataControl _metadataControl;
        private NuGetDeployControl _deployControl;

        public DeployWizard()
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

        public DeployWizard(ManifestMetadata metadata, ProjectAssemblyInfo assemblyInfo, PackageProjectProperties ppp) : this()
        {
            _metadata = metadata;
            _assemblyInfo = assemblyInfo;
            _ppp = ppp;
            _packageOldVersion = _metadata.Version;
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
            var ver = _assemblyInfo?.Version ?? _ppp.AssemblyVersion;
            if (_metadata.Version.IsEmptyOrPlaceHolder())
                _metadata.Version = ver.Replace(".*", "");
            if (_metadata.Title.IsEmptyOrPlaceHolder())
                _metadata.Title = _metadata.Id;
            _metadataControl.ManifestMetadata = _metadata;
            _metadataControl.AssemblyInfo = _assemblyInfo;

         
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
            if (_assemblyInfo != null)
            {
                SaveAssemblyInfo();
                SaveNuSpec();
            }
            else
            {
                SavePackageInfo();
            }
            if (!Build())
                return;
            EnsureOutputDir();
            Pack();

            SyncVersionToDependency();
            SaveNuGetConfig();
            SaveProjectConfig();
        }


        private void SaveAssemblyInfo()
        {
            //_assemblyInfo.Version = _assemblyInfo.FileVersion = txtAssemblyVersion.Text.Trim();
            //_assemblyInfo.Title = textBoxTitle.Text.Trim();
            //_assemblyInfo.Description = textBoxDescription.Text.Trim();
            _assemblyInfo.Company = _metadata.Owners;
            _assemblyInfo.Save(true);
        }

        void SavePackageInfo()
        {
            _metadata.SyncToPackageProjectProperties(_ppp);
            _project.SavePackageProjectProperties(_ppp);
        }

        private void SyncVersionToDependency()
        {
            if (_packageOldVersion == _metadata.Version)
                return;
            NuGetExtensions.UpdateDependencyInSolution(_metadata.Id, _metadata.Version);
        }

        private void SaveNuSpec()
        {
            //if (_metadata.Version != txtPackageVersion.Text.Trim())
            //{
            //    _packageOldVersion = _metadata.Version;
            //    _metadata.Version = txtPackageVersion.Text.Trim();
            //}

            //_metadata.Id = textBoxId.Text.Trim();
            //_metadata.Title = textBoxTitle.Text.Trim();
            //_metadata.Authors = textBoxAuthors.Text.Trim();
            //_metadata.Owners = textBoxOwners.Text.Trim();
            //_metadata.Description = XmlTextFormatter.Encode(textBoxDescription.Text.Trim());
            //_metadata.ReleaseNotes = XmlTextFormatter.Encode(txtNote.Text);

          
          _project.UpdateNuspec(_metadata);
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

                script.AppendFormat(@"""{0}"" push ""{1}"" -source {2} {3}", nugetExe, GetLastPackage(), deployVM.NuGetServer, deployVM.ApiKey);
            }

            CmdUtil.RunCmd(script.ToString());

            ShowPackages();

            if (chkSymbol.Checked && !string.IsNullOrWhiteSpace(deployVM.SymbolServer))
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
            var deployVM = _deployControl.ViewModel;
            var script = new StringBuilder();
            script.AppendLine();
            script.AppendFormat("nuget SetApiKey {0}", deployVM.ApiKey);
            script.AppendLine();
            script.AppendFormat("nuget push {0} -source {1}",GetLastSymbolPackage(), deployVM.SymbolServer);
            CmdUtil.RunCmd(script.ToString());
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