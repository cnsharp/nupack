namespace CnSharp.VisualStudio.NuPack.NuGets
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
            this.textBoxOwners = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.btnOpenCommonAssemblyInfo = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAuthors = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.chkSymbol = new System.Windows.Forms.CheckBox();
            this.chkForceEnglishOutput = new System.Windows.Forms.CheckBox();
            this.chkIncludeReferencedProjects = new System.Windows.Forms.CheckBox();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.textBoxSymbolServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.checkBoxNugetLogin = new System.Windows.Forms.CheckBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.openNugetExeDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openAssemblyInfoFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(440, 190);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(251, 135);
            this.txtNote.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 25;
            this.label3.Text = "Release Note:";
            // 
            // txtPackageVersion
            // 
            this.txtPackageVersion.Location = new System.Drawing.Point(445, 136);
            this.txtPackageVersion.Name = "txtPackageVersion";
            this.txtPackageVersion.Size = new System.Drawing.Size(251, 23);
            this.txtPackageVersion.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(387, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Package Version:";
            // 
            // txtAssemblyVersion
            // 
            this.txtAssemblyVersion.Location = new System.Drawing.Point(86, 136);
            this.txtAssemblyVersion.Name = "txtAssemblyVersion";
            this.txtAssemblyVersion.Size = new System.Drawing.Size(246, 23);
            this.txtAssemblyVersion.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 105);
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
            this.chkOpenDir.Location = new System.Drawing.Point(158, 186);
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
            this.btnOpenOutputDir.Location = new System.Drawing.Point(464, 137);
            this.btnOpenOutputDir.Name = "btnOpenOutputDir";
            this.btnOpenOutputDir.Size = new System.Drawing.Size(23, 25);
            this.btnOpenOutputDir.TabIndex = 19;
            this.btnOpenOutputDir.UseVisualStyleBackColor = true;
            this.btnOpenOutputDir.Click += new System.EventHandler(this.btnOpenOutputDir_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(105, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Package Output directory:";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Location = new System.Drawing.Point(158, 139);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(276, 23);
            this.txtOutputDir.TabIndex = 1;
            // 
            // btnOpenNuGetExe
            // 
            this.btnOpenNuGetExe.FlatAppearance.BorderSize = 0;
            this.btnOpenNuGetExe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenNuGetExe.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenNuGetExe.Location = new System.Drawing.Point(464, 48);
            this.btnOpenNuGetExe.Name = "btnOpenNuGetExe";
            this.btnOpenNuGetExe.Size = new System.Drawing.Size(23, 25);
            this.btnOpenNuGetExe.TabIndex = 17;
            this.btnOpenNuGetExe.UseVisualStyleBackColor = true;
            this.btnOpenNuGetExe.Click += new System.EventHandler(this.btnOpenNuGetExe_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(105, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "NuGet Path:";
            // 
            // txtNugetPath
            // 
            this.txtNugetPath.Location = new System.Drawing.Point(158, 48);
            this.txtNugetPath.Name = "txtNugetPath";
            this.txtNugetPath.Size = new System.Drawing.Size(276, 23);
            this.txtNugetPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "NuGet Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "API Key:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(137, 106);
            this.txtKey.Name = "txtKey";
            this.txtKey.PasswordChar = '*';
            this.txtKey.Size = new System.Drawing.Size(278, 23);
            this.txtKey.TabIndex = 1;
            // 
            // sourceBox
            // 
            this.sourceBox.FormattingEnabled = true;
            this.sourceBox.Location = new System.Drawing.Point(137, 38);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(278, 23);
            this.sourceBox.TabIndex = 0;
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
            this.stepWizardControl.Size = new System.Drawing.Size(953, 488);
            this.stepWizardControl.StepListFont = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.stepWizardControl.TabIndex = 27;
            this.stepWizardControl.Text = "Pack & Deploy";
            this.stepWizardControl.Title = "Pack & Deploy";
            this.stepWizardControl.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("stepWizardControl.TitleIcon")));
            // 
            // wizardPage1
            // 
            this.wizardPage1.AllowBack = false;
            this.wizardPage1.Controls.Add(this.textBoxOwners);
            this.wizardPage1.Controls.Add(this.label13);
            this.wizardPage1.Controls.Add(this.label12);
            this.wizardPage1.Controls.Add(this.textBoxTitle);
            this.wizardPage1.Controls.Add(this.btnOpenCommonAssemblyInfo);
            this.wizardPage1.Controls.Add(this.label11);
            this.wizardPage1.Controls.Add(this.textBoxDescription);
            this.wizardPage1.Controls.Add(this.label7);
            this.wizardPage1.Controls.Add(this.label6);
            this.wizardPage1.Controls.Add(this.textBoxAuthors);
            this.wizardPage1.Controls.Add(this.textBoxId);
            this.wizardPage1.Controls.Add(this.label8);
            this.wizardPage1.Controls.Add(this.txtAssemblyVersion);
            this.wizardPage1.Controls.Add(this.txtPackageVersion);
            this.wizardPage1.Controls.Add(this.label4);
            this.wizardPage1.Controls.Add(this.label3);
            this.wizardPage1.Controls.Add(this.txtNote);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextPage = this.wizardPage2;
            this.wizardPage1.Size = new System.Drawing.Size(755, 334);
            this.stepWizardControl.SetStepText(this.wizardPage1, "Assembly Info");
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.Text = "Assembly Info";
            // 
            // textBoxOwners
            // 
            this.textBoxOwners.Location = new System.Drawing.Point(445, 58);
            this.textBoxOwners.Name = "textBoxOwners";
            this.textBoxOwners.Size = new System.Drawing.Size(246, 23);
            this.textBoxOwners.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(387, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 15);
            this.label13.TabIndex = 37;
            this.label13.Text = "Owners:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(387, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 15);
            this.label12.TabIndex = 36;
            this.label12.Text = "Title:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(445, 23);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(246, 23);
            this.textBoxTitle.TabIndex = 1;
            // 
            // btnOpenCommonAssemblyInfo
            // 
            this.btnOpenCommonAssemblyInfo.FlatAppearance.BorderSize = 0;
            this.btnOpenCommonAssemblyInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenCommonAssemblyInfo.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenCommonAssemblyInfo.Location = new System.Drawing.Point(709, 60);
            this.btnOpenCommonAssemblyInfo.Name = "btnOpenCommonAssemblyInfo";
            this.btnOpenCommonAssemblyInfo.Size = new System.Drawing.Size(23, 25);
            this.btnOpenCommonAssemblyInfo.TabIndex = 34;
            this.btnOpenCommonAssemblyInfo.UseVisualStyleBackColor = true;
            this.btnOpenCommonAssemblyInfo.Click += new System.EventHandler(this.btnOpenCommonAssemblyInfo_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 168);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 15);
            this.label11.TabIndex = 33;
            this.label11.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(86, 196);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(246, 129);
            this.textBoxDescription.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 31;
            this.label7.Text = "Authors:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 15);
            this.label6.TabIndex = 30;
            this.label6.Text = "ID:";
            // 
            // textBoxAuthors
            // 
            this.textBoxAuthors.Location = new System.Drawing.Point(86, 60);
            this.textBoxAuthors.Name = "textBoxAuthors";
            this.textBoxAuthors.Size = new System.Drawing.Size(246, 23);
            this.textBoxAuthors.TabIndex = 2;
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(86, 23);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(246, 23);
            this.textBoxId.TabIndex = 0;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.chkSymbol);
            this.wizardPage2.Controls.Add(this.chkForceEnglishOutput);
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
            this.wizardPage2.Size = new System.Drawing.Size(755, 334);
            this.stepWizardControl.SetStepText(this.wizardPage2, "Package Settings");
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.Text = "Package Settings";
            // 
            // chkSymbol
            // 
            this.chkSymbol.AutoSize = true;
            this.chkSymbol.Location = new System.Drawing.Point(158, 258);
            this.chkSymbol.Name = "chkSymbol";
            this.chkSymbol.Size = new System.Drawing.Size(169, 19);
            this.chkSymbol.TabIndex = 4;
            this.chkSymbol.Text = "Creating a symbol package";
            this.chkSymbol.UseVisualStyleBackColor = true;
            // 
            // chkForceEnglishOutput
            // 
            this.chkForceEnglishOutput.AutoSize = true;
            this.chkForceEnglishOutput.Checked = true;
            this.chkForceEnglishOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForceEnglishOutput.Location = new System.Drawing.Point(158, 290);
            this.chkForceEnglishOutput.Name = "chkForceEnglishOutput";
            this.chkForceEnglishOutput.Size = new System.Drawing.Size(131, 19);
            this.chkForceEnglishOutput.TabIndex = 5;
            this.chkForceEnglishOutput.Text = "ForceEnglishOutput";
            this.chkForceEnglishOutput.UseVisualStyleBackColor = true;
            // 
            // chkIncludeReferencedProjects
            // 
            this.chkIncludeReferencedProjects.AutoSize = true;
            this.chkIncludeReferencedProjects.Location = new System.Drawing.Point(158, 225);
            this.chkIncludeReferencedProjects.Name = "chkIncludeReferencedProjects";
            this.chkIncludeReferencedProjects.Size = new System.Drawing.Size(166, 19);
            this.chkIncludeReferencedProjects.TabIndex = 3;
            this.chkIncludeReferencedProjects.Text = "IncludeReferencedProjects";
            this.chkIncludeReferencedProjects.UseVisualStyleBackColor = true;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.textBoxSymbolServer);
            this.wizardPage3.Controls.Add(this.label5);
            this.wizardPage3.Controls.Add(this.labelLogin);
            this.wizardPage3.Controls.Add(this.textBoxLogin);
            this.wizardPage3.Controls.Add(this.checkBoxNugetLogin);
            this.wizardPage3.Controls.Add(this.chkRemember);
            this.wizardPage3.Controls.Add(this.label1);
            this.wizardPage3.Controls.Add(this.sourceBox);
            this.wizardPage3.Controls.Add(this.label2);
            this.wizardPage3.Controls.Add(this.txtKey);
            this.wizardPage3.IsFinishPage = true;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(755, 334);
            this.stepWizardControl.SetStepText(this.wizardPage3, "Deploy");
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Deploy";
            // 
            // textBoxSymbolServer
            // 
            this.textBoxSymbolServer.Location = new System.Drawing.Point(137, 186);
            this.textBoxSymbolServer.Name = "textBoxSymbolServer";
            this.textBoxSymbolServer.Size = new System.Drawing.Size(278, 23);
            this.textBoxSymbolServer.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Symbol Server:";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(100, 260);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(40, 15);
            this.labelLogin.TabIndex = 28;
            this.labelLogin.Text = "Login:";
            this.labelLogin.Visible = false;
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(137, 287);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(278, 23);
            this.textBoxLogin.TabIndex = 4;
            this.textBoxLogin.Visible = false;
            // 
            // checkBoxNugetLogin
            // 
            this.checkBoxNugetLogin.AutoSize = true;
            this.checkBoxNugetLogin.Location = new System.Drawing.Point(103, 230);
            this.checkBoxNugetLogin.Name = "checkBoxNugetLogin";
            this.checkBoxNugetLogin.Size = new System.Drawing.Size(128, 19);
            this.checkBoxNugetLogin.TabIndex = 8;
            this.checkBoxNugetLogin.Text = "Use NuGet V2 login";
            this.checkBoxNugetLogin.UseVisualStyleBackColor = true;
            this.checkBoxNugetLogin.CheckedChanged += new System.EventHandler(this.checkBoxNugetLogin_CheckedChanged);
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(455, 106);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(94, 19);
            this.chkRemember.TabIndex = 2;
            this.chkRemember.Text = "Remember it";
            this.chkRemember.UseVisualStyleBackColor = true;
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
            // DeployWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(953, 488);
            this.Controls.Add(this.stepWizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeployWizard";
            this.Text = "Deploy Wizard";
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
        private System.Windows.Forms.OpenFileDialog openNugetExeDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.CheckBox chkIncludeReferencedProjects;
        private System.Windows.Forms.CheckBox checkBoxNugetLogin;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.CheckBox chkForceEnglishOutput;
        private System.Windows.Forms.CheckBox chkSymbol;
        private System.Windows.Forms.TextBox textBoxSymbolServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAuthors;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnOpenCommonAssemblyInfo;
        private System.Windows.Forms.OpenFileDialog openAssemblyInfoFileDialog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxOwners;
        private System.Windows.Forms.Label label13;
    }
}