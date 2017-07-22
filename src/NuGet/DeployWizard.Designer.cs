namespace CnSharp.VisualStudio.NuPack.NuGet
{
    partial class DeployWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeployWizard));
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPackageVersion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAssemblyVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkOpenDir = new System.Windows.Forms.CheckBox();
            this.btnOpenOutputDir = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.btnOpenNuGetExe = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNugetPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.sourceBox = new System.Windows.Forms.ComboBox();
            this.stepWizardControl = new AeroWizard.StepWizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.chkSyncDep = new System.Windows.Forms.CheckBox();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.chkIncludeReferencedProjects = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(31, 186);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(343, 94);
            this.txtNote.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 25;
            this.label3.Text = "Release Note:";
            // 
            // txtPackageVersion
            // 
            this.txtPackageVersion.Location = new System.Drawing.Point(177, 67);
            this.txtPackageVersion.Name = "txtPackageVersion";
            this.txtPackageVersion.Size = new System.Drawing.Size(197, 23);
            this.txtPackageVersion.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Package Version:";
            // 
            // txtAssemblyVersion
            // 
            this.txtAssemblyVersion.Location = new System.Drawing.Point(177, 8);
            this.txtAssemblyVersion.Name = "txtAssemblyVersion";
            this.txtAssemblyVersion.Size = new System.Drawing.Size(197, 23);
            this.txtAssemblyVersion.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Assembly Version:";
            // 
            // chkOpenDir
            // 
            this.chkOpenDir.AutoSize = true;
            this.chkOpenDir.Checked = true;
            this.chkOpenDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenDir.Location = new System.Drawing.Point(40, 180);
            this.chkOpenDir.Name = "chkOpenDir";
            this.chkOpenDir.Size = new System.Drawing.Size(201, 19);
            this.chkOpenDir.TabIndex = 20;
            this.chkOpenDir.Text = "Open output directory after build";
            this.chkOpenDir.UseVisualStyleBackColor = true;
            // 
            // btnOpenOutputDir
            // 
            this.btnOpenOutputDir.FlatAppearance.BorderSize = 0;
            this.btnOpenOutputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenOutputDir.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenOutputDir.Location = new System.Drawing.Point(343, 131);
            this.btnOpenOutputDir.Name = "btnOpenOutputDir";
            this.btnOpenOutputDir.Size = new System.Drawing.Size(23, 25);
            this.btnOpenOutputDir.TabIndex = 19;
            this.btnOpenOutputDir.UseVisualStyleBackColor = true;
            this.btnOpenOutputDir.Click += new System.EventHandler(this.btnOpenOutputDir_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Package Output directory:";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Location = new System.Drawing.Point(40, 130);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(276, 23);
            this.txtOutputDir.TabIndex = 16;
            // 
            // btnOpenNuGetExe
            // 
            this.btnOpenNuGetExe.FlatAppearance.BorderSize = 0;
            this.btnOpenNuGetExe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenNuGetExe.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenNuGetExe.Location = new System.Drawing.Point(343, 46);
            this.btnOpenNuGetExe.Name = "btnOpenNuGetExe";
            this.btnOpenNuGetExe.Size = new System.Drawing.Size(23, 25);
            this.btnOpenNuGetExe.TabIndex = 17;
            this.btnOpenNuGetExe.UseVisualStyleBackColor = true;
            this.btnOpenNuGetExe.Click += new System.EventHandler(this.btnOpenNuGetExe_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "NuGet Path:";
            // 
            // txtNugetPath
            // 
            this.txtNugetPath.Location = new System.Drawing.Point(40, 44);
            this.txtNugetPath.Name = "txtNugetPath";
            this.txtNugetPath.Size = new System.Drawing.Size(276, 23);
            this.txtNugetPath.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "NuGet Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "API Key:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(36, 158);
            this.txtKey.Name = "txtKey";
            this.txtKey.PasswordChar = '*';
            this.txtKey.Size = new System.Drawing.Size(278, 23);
            this.txtKey.TabIndex = 5;
            // 
            // sourceBox
            // 
            this.sourceBox.FormattingEnabled = true;
            this.sourceBox.Location = new System.Drawing.Point(36, 46);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(278, 23);
            this.sourceBox.TabIndex = 4;
            this.sourceBox.SelectedIndexChanged += new System.EventHandler(this.sourceBox_SelectedIndexChanged);
            // 
            // stepWizardControl
            // 
            this.stepWizardControl.BackColor = System.Drawing.Color.White;
            this.stepWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepWizardControl.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl.Name = "stepWizardControl";
            this.stepWizardControl.Pages.Add(this.wizardPage1);
            this.stepWizardControl.Pages.Add(this.wizardPage2);
            this.stepWizardControl.Pages.Add(this.wizardPage3);
            this.stepWizardControl.Size = new System.Drawing.Size(645, 464);
            this.stepWizardControl.StepListFont = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.stepWizardControl.TabIndex = 27;
            this.stepWizardControl.Text = "Pack / Deploy";
            this.stepWizardControl.Title = "Pack / Deploy";
            this.stepWizardControl.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("stepWizardControl.TitleIcon")));
            // 
            // wizardPage1
            // 
            this.wizardPage1.AllowBack = false;
            this.wizardPage1.Controls.Add(this.chkSyncDep);
            this.wizardPage1.Controls.Add(this.txtNote);
            this.wizardPage1.Controls.Add(this.label8);
            this.wizardPage1.Controls.Add(this.txtAssemblyVersion);
            this.wizardPage1.Controls.Add(this.txtPackageVersion);
            this.wizardPage1.Controls.Add(this.label4);
            this.wizardPage1.Controls.Add(this.label3);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextPage = this.wizardPage2;
            this.wizardPage1.Size = new System.Drawing.Size(447, 310);
            this.stepWizardControl.SetStepText(this.wizardPage1, "Release Info");
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.Text = "Release Info";
            // 
            // chkSyncDep
            // 
            this.chkSyncDep.AutoSize = true;
            this.chkSyncDep.Location = new System.Drawing.Point(177, 115);
            this.chkSyncDep.Name = "chkSyncDep";
            this.chkSyncDep.Size = new System.Drawing.Size(179, 19);
            this.chkSyncDep.TabIndex = 27;
            this.chkSyncDep.Text = "Sync to projects referenced it";
            this.chkSyncDep.UseVisualStyleBackColor = true;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.chkIncludeReferencedProjects);
            this.wizardPage2.Controls.Add(this.label9);
            this.wizardPage2.Controls.Add(this.txtNugetPath);
            this.wizardPage2.Controls.Add(this.btnOpenNuGetExe);
            this.wizardPage2.Controls.Add(this.chkOpenDir);
            this.wizardPage2.Controls.Add(this.label10);
            this.wizardPage2.Controls.Add(this.txtOutputDir);
            this.wizardPage2.Controls.Add(this.btnOpenOutputDir);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.NextPage = this.wizardPage3;
            this.wizardPage2.Size = new System.Drawing.Size(447, 310);
            this.stepWizardControl.SetStepText(this.wizardPage2, "Pack Settings");
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.Text = "Pack Settings";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.chkRemember);
            this.wizardPage3.Controls.Add(this.label1);
            this.wizardPage3.Controls.Add(this.sourceBox);
            this.wizardPage3.Controls.Add(this.label2);
            this.wizardPage3.Controls.Add(this.txtKey);
            this.wizardPage3.IsFinishPage = true;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(447, 310);
            this.stepWizardControl.SetStepText(this.wizardPage3, "Deploy");
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Deploy";
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(36, 224);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(94, 19);
            this.chkRemember.TabIndex = 7;
            this.chkRemember.Text = "Remember it";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "nuget.exe";
            this.openFileDialog.Title = "Open nuget.exe";
            // 
            // chkIncludeReferencedProjects
            // 
            this.chkIncludeReferencedProjects.AutoSize = true;
            this.chkIncludeReferencedProjects.Location = new System.Drawing.Point(40, 231);
            this.chkIncludeReferencedProjects.Name = "chkIncludeReferencedProjects";
            this.chkIncludeReferencedProjects.Size = new System.Drawing.Size(166, 19);
            this.chkIncludeReferencedProjects.TabIndex = 21;
            this.chkIncludeReferencedProjects.Text = "IncludeReferencedProjects";
            this.chkIncludeReferencedProjects.UseVisualStyleBackColor = true;
            // 
            // DeployWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(645, 464);
            this.Controls.Add(this.stepWizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeployWizard";
            this.Text = "DeployWizard";
            this.Load += new System.EventHandler(this.DeployWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPackageVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAssemblyVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkOpenDir;
        private System.Windows.Forms.Button btnOpenOutputDir;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button btnOpenNuGetExe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNugetPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.ComboBox sourceBox;
        private AeroWizard.StepWizardControl stepWizardControl;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage2;
        private AeroWizard.WizardPage wizardPage3;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.CheckBox chkSyncDep;
        private System.Windows.Forms.CheckBox chkIncludeReferencedProjects;
    }
}