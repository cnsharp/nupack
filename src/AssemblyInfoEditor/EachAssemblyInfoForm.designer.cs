namespace CnSharp.VisualStudio.NuPack.AssemblyInfoEditor
{
	partial class EachAssemblyInfoForm
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
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
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
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.projectGrid = new System.Windows.Forms.DataGridView();
            this.ColProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCopyright = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.projectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlCopy = new System.Windows.Forms.Panel();
            this.chkFileVersion = new System.Windows.Forms.CheckBox();
            this.chkVersion = new System.Windows.Forms.CheckBox();
            this.chkCopyright = new System.Windows.Forms.CheckBox();
            this.chkCompany = new System.Windows.Forms.CheckBox();
            this.chkProduct = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.pnlCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(933, 439);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.projectGrid);
            this.groupBox1.Location = new System.Drawing.Point(27, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1002, 350);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projects";
            // 
            // projectGrid
            // 
            this.projectGrid.AllowUserToAddRows = false;
            this.projectGrid.AllowUserToDeleteRows = false;
            this.projectGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.projectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColProjectName,
            this.ColProductName,
            this.ColCompany,
            this.ColCopyright,
            this.ColVersion,
            this.ColFileVersion});
            this.projectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectGrid.Location = new System.Drawing.Point(3, 17);
            this.projectGrid.Name = "projectGrid";
            this.projectGrid.RowTemplate.Height = 23;
            this.projectGrid.Size = new System.Drawing.Size(996, 330);
            this.projectGrid.TabIndex = 0;
            this.projectGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectGrid_CellValidated);
            this.projectGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.projectGrid_CellValidating);
            // 
            // ColProjectName
            // 
            this.ColProjectName.DataPropertyName = "ProjectName";
            this.ColProjectName.HeaderText = "Project Name";
            this.ColProjectName.Name = "ColProjectName";
            this.ColProjectName.ReadOnly = true;
            // 
            // ColProductName
            // 
            this.ColProductName.DataPropertyName = "ProductName";
            this.ColProductName.HeaderText = "Product Name";
            this.ColProductName.Name = "ColProductName";
            // 
            // ColCompany
            // 
            this.ColCompany.DataPropertyName = "Company";
            this.ColCompany.HeaderText = "Company";
            this.ColCompany.Name = "ColCompany";
            // 
            // ColCopyright
            // 
            this.ColCopyright.DataPropertyName = "Copyright";
            this.ColCopyright.HeaderText = "Copyright";
            this.ColCopyright.Name = "ColCopyright";
            // 
            // ColVersion
            // 
            this.ColVersion.DataPropertyName = "Version";
            this.ColVersion.HeaderText = "Version";
            this.ColVersion.Name = "ColVersion";
            // 
            // ColFileVersion
            // 
            this.ColFileVersion.DataPropertyName = "FileVersion";
            this.ColFileVersion.HeaderText = "File Version";
            this.ColFileVersion.Name = "ColFileVersion";
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.Location = new System.Drawing.Point(796, 439);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Save and Exit";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1051, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // pnlCopy
            // 
            this.pnlCopy.Controls.Add(this.chkFileVersion);
            this.pnlCopy.Controls.Add(this.chkVersion);
            this.pnlCopy.Controls.Add(this.chkCopyright);
            this.pnlCopy.Controls.Add(this.chkCompany);
            this.pnlCopy.Controls.Add(this.chkProduct);
            this.pnlCopy.Controls.Add(this.label1);
            this.pnlCopy.Location = new System.Drawing.Point(19, 384);
            this.pnlCopy.Name = "pnlCopy";
            this.pnlCopy.Size = new System.Drawing.Size(1008, 37);
            this.pnlCopy.TabIndex = 15;
            // 
            // chkFileVersion
            // 
            this.chkFileVersion.AutoSize = true;
            this.chkFileVersion.Location = new System.Drawing.Point(835, 9);
            this.chkFileVersion.Name = "chkFileVersion";
            this.chkFileVersion.Size = new System.Drawing.Size(96, 16);
            this.chkFileVersion.TabIndex = 24;
            this.chkFileVersion.Tag = "FileVersion";
            this.chkFileVersion.Text = "File Version";
            this.chkFileVersion.UseVisualStyleBackColor = true;
            // 
            // chkVersion
            // 
            this.chkVersion.AutoSize = true;
            this.chkVersion.Location = new System.Drawing.Point(684, 9);
            this.chkVersion.Name = "chkVersion";
            this.chkVersion.Size = new System.Drawing.Size(66, 16);
            this.chkVersion.TabIndex = 23;
            this.chkVersion.Tag = "Version";
            this.chkVersion.Text = "Version";
            this.chkVersion.UseVisualStyleBackColor = true;
            // 
            // chkCopyright
            // 
            this.chkCopyright.AutoSize = true;
            this.chkCopyright.Location = new System.Drawing.Point(511, 9);
            this.chkCopyright.Name = "chkCopyright";
            this.chkCopyright.Size = new System.Drawing.Size(78, 16);
            this.chkCopyright.TabIndex = 22;
            this.chkCopyright.Tag = "Copyright";
            this.chkCopyright.Text = "Copyright";
            this.chkCopyright.UseVisualStyleBackColor = true;
            // 
            // chkCompany
            // 
            this.chkCompany.AutoSize = true;
            this.chkCompany.Location = new System.Drawing.Point(379, 10);
            this.chkCompany.Name = "chkCompany";
            this.chkCompany.Size = new System.Drawing.Size(66, 16);
            this.chkCompany.TabIndex = 21;
            this.chkCompany.Tag = "Company";
            this.chkCompany.Text = "Company";
            this.chkCompany.UseVisualStyleBackColor = true;
            // 
            // chkProduct
            // 
            this.chkProduct.AutoSize = true;
            this.chkProduct.Location = new System.Drawing.Point(218, 10);
            this.chkProduct.Name = "chkProduct";
            this.chkProduct.Size = new System.Drawing.Size(96, 16);
            this.chkProduct.TabIndex = 20;
            this.chkProduct.Tag = "ProductName";
            this.chkProduct.Text = "Product Name";
            this.chkProduct.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "Same as the first project";
            // 
            // EachAssemblyInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 488);
            this.Controls.Add(this.pnlCopy);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EachAssemblyInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Assembly Info.";
            this.Load += new System.EventHandler(this.FormVersionLoad);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlCopy.ResumeLayout(false);
            this.pnlCopy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button button2;
  private System.Windows.Forms.GroupBox groupBox1;
  private System.Windows.Forms.DataGridView projectGrid;
  private System.Windows.Forms.Button btnOK;
  private System.Windows.Forms.BindingSource projectBindingSource;
  private System.Windows.Forms.DataGridViewTextBoxColumn ColProjectName;
  private System.Windows.Forms.DataGridViewTextBoxColumn ColProductName;
  private System.Windows.Forms.DataGridViewTextBoxColumn ColCompany;
  private System.Windows.Forms.DataGridViewTextBoxColumn ColCopyright;
  private System.Windows.Forms.DataGridViewTextBoxColumn ColVersion;
  private System.Windows.Forms.DataGridViewTextBoxColumn ColFileVersion;
  private System.Windows.Forms.StatusStrip statusStrip1;
  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
  private System.Windows.Forms.Panel pnlCopy;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.CheckBox chkFileVersion;
  private System.Windows.Forms.CheckBox chkVersion;
  private System.Windows.Forms.CheckBox chkCopyright;
  private System.Windows.Forms.CheckBox chkCompany;
  private System.Windows.Forms.CheckBox chkProduct;
	}
}