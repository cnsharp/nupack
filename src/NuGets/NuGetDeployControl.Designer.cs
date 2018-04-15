namespace CnSharp.VisualStudio.NuPack.NuGets
{
    partial class NuGetDeployControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxSymbolServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.checkBoxNugetLogin = new System.Windows.Forms.CheckBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sourceBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxSymbolServer
            // 
            this.textBoxSymbolServer.Location = new System.Drawing.Point(101, 124);
            this.textBoxSymbolServer.Name = "textBoxSymbolServer";
            this.textBoxSymbolServer.Size = new System.Drawing.Size(412, 20);
            this.textBoxSymbolServer.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Symbol Server:";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(39, 225);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(36, 13);
            this.labelLogin.TabIndex = 38;
            this.labelLogin.Text = "Login:";
            this.labelLogin.Visible = false;
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(101, 222);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(412, 20);
            this.textBoxLogin.TabIndex = 35;
            this.textBoxLogin.Visible = false;
            // 
            // checkBoxNugetLogin
            // 
            this.checkBoxNugetLogin.AutoSize = true;
            this.checkBoxNugetLogin.Location = new System.Drawing.Point(3, 182);
            this.checkBoxNugetLogin.Name = "checkBoxNugetLogin";
            this.checkBoxNugetLogin.Size = new System.Drawing.Size(120, 17);
            this.checkBoxNugetLogin.TabIndex = 37;
            this.checkBoxNugetLogin.Text = "Use NuGet V2 login";
            this.checkBoxNugetLogin.UseVisualStyleBackColor = true;
            this.checkBoxNugetLogin.CheckedChanged += new System.EventHandler(this.checkBoxNugetLogin_CheckedChanged);
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(537, 64);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(85, 17);
            this.chkRemember.TabIndex = 32;
            this.chkRemember.Text = "Remember it";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "NuGet Server:";
            // 
            // sourceBox
            // 
            this.sourceBox.FormattingEnabled = true;
            this.sourceBox.Location = new System.Drawing.Point(101, 3);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(412, 21);
            this.sourceBox.TabIndex = 30;
            this.sourceBox.SelectedIndexChanged += new System.EventHandler(this.sourceBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "API Key:";
            // 
            // txtKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(101, 60);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.PasswordChar = '*';
            this.textBoxApiKey.Size = new System.Drawing.Size(412, 20);
            this.textBoxApiKey.TabIndex = 31;
            // 
            // NuGetDeployControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxSymbolServer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.checkBoxNugetLogin);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxApiKey);
            this.Name = "NuGetDeployControl";
            this.Size = new System.Drawing.Size(620, 247);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSymbolServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.CheckBox checkBoxNugetLogin;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sourceBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxApiKey;
    }
}
