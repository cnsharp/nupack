namespace CnSharp.VisualStudio.NuPack.NuGets
{
    partial class MsbuildDeployWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsbuildDeployWizard));
            this.chkOpenDir = new System.Windows.Forms.CheckBox();
            this.btnOpenOutputDir = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.btnOpenNuGetExe = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNugetPath = new System.Windows.Forms.TextBox();
            this.stepWizardControl = new AeroWizard.StepWizardControl();
            this.wizardPageMetadata = new AeroWizard.WizardPage();
            this.panelPackageInfo = new System.Windows.Forms.Panel();
            this.wizardPageOptions = new AeroWizard.WizardPage();
            this.chkSymbol = new System.Windows.Forms.CheckBox();
            this.chkForceEnglishOutput = new System.Windows.Forms.CheckBox();
            this.chkIncludeReferencedProjects = new System.Windows.Forms.CheckBox();
            this.wizardPageDeploy = new AeroWizard.WizardPage();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.openNugetExeDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openAssemblyInfoFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).BeginInit();
            this.wizardPageMetadata.SuspendLayout();
            this.wizardPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // chkOpenDir
            // 
            this.chkOpenDir.AutoSize = true;
            this.chkOpenDir.Checked = true;
            this.chkOpenDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenDir.Location = new System.Drawing.Point(123, 161);
            this.chkOpenDir.Name = "chkOpenDir";
            this.chkOpenDir.Size = new System.Drawing.Size(201, 19);
            this.chkOpenDir.TabIndex = 2;
            this.chkOpenDir.Text = "Open output directory after build";
            this.chkOpenDir.UseVisualStyleBackColor = true;
            // 
            // btnOpenOutputDir
            // 
            this.btnOpenOutputDir.FlatAppearance.BorderSize = 0;
            this.btnOpenOutputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenOutputDir.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenOutputDir.Location = new System.Drawing.Point(707, 98);
            this.btnOpenOutputDir.Name = "btnOpenOutputDir";
            this.btnOpenOutputDir.Size = new System.Drawing.Size(23, 25);
            this.btnOpenOutputDir.TabIndex = 7;
            this.btnOpenOutputDir.UseVisualStyleBackColor = true;
            this.btnOpenOutputDir.Click += new System.EventHandler(this.btnOpenOutputDir_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Package Output directory:";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Location = new System.Drawing.Point(123, 98);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(559, 23);
            this.txtOutputDir.TabIndex = 1;
            // 
            // btnOpenNuGetExe
            // 
            this.btnOpenNuGetExe.FlatAppearance.BorderSize = 0;
            this.btnOpenNuGetExe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenNuGetExe.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenNuGetExe.Location = new System.Drawing.Point(707, 20);
            this.btnOpenNuGetExe.Name = "btnOpenNuGetExe";
            this.btnOpenNuGetExe.Size = new System.Drawing.Size(23, 25);
            this.btnOpenNuGetExe.TabIndex = 6;
            this.btnOpenNuGetExe.UseVisualStyleBackColor = true;
            this.btnOpenNuGetExe.Click += new System.EventHandler(this.btnOpenNuGetExe_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "NuGet Path:";
            // 
            // txtNugetPath
            // 
            this.txtNugetPath.Location = new System.Drawing.Point(123, 22);
            this.txtNugetPath.Name = "txtNugetPath";
            this.txtNugetPath.Size = new System.Drawing.Size(559, 23);
            this.txtNugetPath.TabIndex = 0;
            // 
            // stepWizardControl
            // 
            this.stepWizardControl.BackColor = System.Drawing.Color.White;
            this.stepWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepWizardControl.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl.Name = "stepWizardControl";
            this.stepWizardControl.Pages.Add(this.wizardPageMetadata);
            this.stepWizardControl.Pages.Add(this.wizardPageOptions);
            this.stepWizardControl.Pages.Add(this.wizardPageDeploy);
            this.stepWizardControl.Size = new System.Drawing.Size(1088, 500);
            this.stepWizardControl.StepListFont = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.stepWizardControl.TabIndex = 27;
            this.stepWizardControl.Text = "Pack & Deploy";
            this.stepWizardControl.Title = "Pack & Deploy";
            this.stepWizardControl.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("stepWizardControl.TitleIcon")));
            // 
            // wizardPageMetadata
            // 
            this.wizardPageMetadata.AllowBack = false;
            this.wizardPageMetadata.Controls.Add(this.panelPackageInfo);
            this.wizardPageMetadata.Name = "wizardPageMetadata";
            this.wizardPageMetadata.NextPage = this.wizardPageOptions;
            this.wizardPageMetadata.Size = new System.Drawing.Size(890, 346);
            this.stepWizardControl.SetStepText(this.wizardPageMetadata, "Metadata");
            this.wizardPageMetadata.TabIndex = 2;
            this.wizardPageMetadata.Text = "Metadata";
            // 
            // panelPackageInfo
            // 
            this.panelPackageInfo.AutoScroll = true;
            this.panelPackageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPackageInfo.Location = new System.Drawing.Point(0, 0);
            this.panelPackageInfo.Name = "panelPackageInfo";
            this.panelPackageInfo.Size = new System.Drawing.Size(890, 346);
            this.panelPackageInfo.TabIndex = 49;
            // 
            // wizardPageOptions
            // 
            this.wizardPageOptions.Controls.Add(this.chkSymbol);
            this.wizardPageOptions.Controls.Add(this.chkForceEnglishOutput);
            this.wizardPageOptions.Controls.Add(this.chkIncludeReferencedProjects);
            this.wizardPageOptions.Controls.Add(this.label9);
            this.wizardPageOptions.Controls.Add(this.txtNugetPath);
            this.wizardPageOptions.Controls.Add(this.btnOpenNuGetExe);
            this.wizardPageOptions.Controls.Add(this.chkOpenDir);
            this.wizardPageOptions.Controls.Add(this.label10);
            this.wizardPageOptions.Controls.Add(this.txtOutputDir);
            this.wizardPageOptions.Controls.Add(this.btnOpenOutputDir);
            this.wizardPageOptions.Name = "wizardPageOptions";
            this.wizardPageOptions.NextPage = this.wizardPageDeploy;
            this.wizardPageOptions.Size = new System.Drawing.Size(890, 346);
            this.stepWizardControl.SetStepText(this.wizardPageOptions, "Options");
            this.wizardPageOptions.TabIndex = 3;
            this.wizardPageOptions.Text = "Options";
            // 
            // chkSymbol
            // 
            this.chkSymbol.AutoSize = true;
            this.chkSymbol.Location = new System.Drawing.Point(123, 209);
            this.chkSymbol.Name = "chkSymbol";
            this.chkSymbol.Size = new System.Drawing.Size(169, 19);
            this.chkSymbol.TabIndex = 4;
            this.chkSymbol.Text = "Creating a symbol package";
            this.chkSymbol.UseVisualStyleBackColor = true;
            // 
            // chkForceEnglishOutput
            // 
            this.chkForceEnglishOutput.AutoSize = true;
            this.chkForceEnglishOutput.Location = new System.Drawing.Point(123, 319);
            this.chkForceEnglishOutput.Name = "chkForceEnglishOutput";
            this.chkForceEnglishOutput.Size = new System.Drawing.Size(131, 19);
            this.chkForceEnglishOutput.TabIndex = 5;
            this.chkForceEnglishOutput.Text = "ForceEnglishOutput";
            this.chkForceEnglishOutput.UseVisualStyleBackColor = true;
            this.chkForceEnglishOutput.Visible = false;
            // 
            // chkIncludeReferencedProjects
            // 
            this.chkIncludeReferencedProjects.AutoSize = true;
            this.chkIncludeReferencedProjects.Location = new System.Drawing.Point(123, 265);
            this.chkIncludeReferencedProjects.Name = "chkIncludeReferencedProjects";
            this.chkIncludeReferencedProjects.Size = new System.Drawing.Size(166, 19);
            this.chkIncludeReferencedProjects.TabIndex = 3;
            this.chkIncludeReferencedProjects.Text = "IncludeReferencedProjects";
            this.chkIncludeReferencedProjects.UseVisualStyleBackColor = true;
            this.chkIncludeReferencedProjects.Visible = false;
            // 
            // wizardPageDeploy
            // 
            this.wizardPageDeploy.IsFinishPage = true;
            this.wizardPageDeploy.Name = "wizardPageDeploy";
            this.wizardPageDeploy.Size = new System.Drawing.Size(890, 346);
            this.stepWizardControl.SetStepText(this.wizardPageDeploy, "Deploy");
            this.wizardPageDeploy.TabIndex = 4;
            this.wizardPageDeploy.Text = "Deploy";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openNugetExeDialog
            // 
            this.openNugetExeDialog.FileName = "nuget.exe";
            this.openNugetExeDialog.Title = "Open nuget.exe";
            // 
            // openAssemblyInfoFileDialog
            // 
            this.openAssemblyInfoFileDialog.DefaultExt = "*.cs|*.vb";
            this.openAssemblyInfoFileDialog.Title = "Open Common Assembly Info File";
            // 
            // MsbuildDeployWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1088, 500);
            this.Controls.Add(this.stepWizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsbuildDeployWizard";
            this.Text = "Deploy Wizard";
            this.Load += new System.EventHandler(this.DeployWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).EndInit();
            this.wizardPageMetadata.ResumeLayout(false);
            this.wizardPageOptions.ResumeLayout(false);
            this.wizardPageOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chkOpenDir;
        private System.Windows.Forms.Button btnOpenOutputDir;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button btnOpenNuGetExe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNugetPath;
        private AeroWizard.StepWizardControl stepWizardControl;
        private AeroWizard.WizardPage wizardPageMetadata;
        private AeroWizard.WizardPage wizardPageOptions;
        private AeroWizard.WizardPage wizardPageDeploy;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.OpenFileDialog openNugetExeDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox chkIncludeReferencedProjects;
        private System.Windows.Forms.CheckBox chkForceEnglishOutput;
        private System.Windows.Forms.CheckBox chkSymbol;
        private System.Windows.Forms.OpenFileDialog openAssemblyInfoFileDialog;
        private System.Windows.Forms.Panel panelPackageInfo;
    }
}