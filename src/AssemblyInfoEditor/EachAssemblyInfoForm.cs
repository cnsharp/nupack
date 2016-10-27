using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.Extensions.SourceControl;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.AssemblyInfoEditor
{
    public partial class EachAssemblyInfoForm : Form
    {
        #region Constants and Fields


        //private readonly Project _startProject;

        private readonly List<ProjectAssemblyInfo> _projectInfos;
        private readonly ProjectAssemblyInfo[] _projectOriginalInfos;

        #endregion

        #region Constructors and Destructors

        public EachAssemblyInfoForm(IEnumerable<Project> refProjects)
        {
            InitializeComponent();
            Icon = Resource.logo;

            projectGrid.AutoGenerateColumns = false;
            RegisterEvents();

             var projects = refProjects.ToList();
             _projectInfos = projects.Select(p => p.GetProjectAssemblyInfo()).ToList();
            _projectOriginalInfos = _projectInfos.Select(p => p.Copy()).ToArray();
            projectBindingSource.DataSource = _projectInfos;
            projectGrid.DataSource = projectBindingSource;

            pnlCopy.Visible = projects.Count > 1;

        }

        private void RegisterEvents()
        {
            chkCompany.CheckedChanged += chkSame_CheckedChanged;
            chkCopyright.CheckedChanged += chkSame_CheckedChanged;
            chkFileVersion.CheckedChanged += chkSame_CheckedChanged;
            chkProduct.CheckedChanged += chkSame_CheckedChanged;
            chkVersion.CheckedChanged += chkSame_CheckedChanged;
        }


        #endregion

        #region Public Properties

     
        public ISourceControl SourceControl { get; set; }

        #endregion

        #region Methods

        private void FormVersionLoad(object sender, EventArgs e)
        {
            ActiveControl = projectGrid;
        }

         void CheckOut(ProjectAssemblyInfo assemblyInfo, string assemblyInfoFile)
        {

            var attr = File.GetAttributes(assemblyInfoFile);
            var isNormal = Contains(attr, FileAttributes.Normal);
            var checkout = false;
            if (SourceControl != null)
            {
              
                    var files =
                       SourceControl.CheckOut(
                            Path.GetDirectoryName(assemblyInfo.Project.DTE.Solution.FullName),
                            assemblyInfoFile);
                    checkout = files > 0; 
            }
            if (checkout || isNormal)
                return;
                   
            File.SetAttributes(assemblyInfoFile, FileAttributes.Normal);
            
        }

        public static bool Contains(Enum keys, Enum flag)
        {
            ulong keysVal = Convert.ToUInt64(keys);
            ulong flagVal = Convert.ToUInt64(flag);

            return (keysVal & flagVal) == flagVal;
        }
        #endregion

        //private void chkSingle_CheckedChanged(object sender, EventArgs e)
        //{
        //    var allVersionControlled = chkAll.Checked;
        //    var i = 0;
        //    foreach (DataGridViewRow row in projectGrid.Rows)
        //    {
        //        if (i > 0)
        //        {
        //            row.DefaultCellStyle.ForeColor = !allVersionControlled ? Color.Gray : SystemColors.WindowText;
        //            row.ReadOnly = !allVersionControlled;
        //        }
        //        i++;
        //    }

        //    chkSame.Enabled = chkAll.Checked;
        //    if(!chkSame.Enabled)
        //        chkSame.Checked = false;
        //}

        private void chkSame_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if(cb.Checked)
                CopyProperties(cb.Tag.ToString());
            else
                RevertProperties(cb.Tag.ToString());
        }

        private void projectGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            projectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
            if (e.FormattedValue == null || string.IsNullOrEmpty(e.FormattedValue.ToString()) ||
                e.FormattedValue.ToString().Trim().Length == 0)
            {
                projectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "*";
                e.Cancel = true;
            }
        }

        private void projectGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            projectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
        }

        private void CopyProperties(string propertyName)
        {
            var cellName = "Col" + propertyName;
            var v = projectGrid.Rows[0].Cells[cellName].Value;
            for (var i = 1; i < projectGrid.RowCount; i++)
            {
                projectGrid.Rows[i].Cells[cellName].Value = v;
            }
            projectGrid.EndEdit();
        }

        private void RevertProperties(string propertyName)
        {
            var cellName = "Col" + propertyName;
           
            for (var i = 1; i < projectGrid.RowCount; i++)
            {
                var info = _projectOriginalInfos[i];
                var v = typeof (ProjectAssemblyInfo).GetProperty(propertyName).GetValue(info, null);
                projectGrid.Rows[i].Cells[cellName].Value = v;
            }
            projectGrid.EndEdit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!projectGrid.EndEdit())
                return;

            projectBindingSource.ResetBindings(false);

            toolStripStatusLabel.Text = "Saving...";
            var i = 0;
            foreach (var assemblyInfo in _projectInfos)
            {
                if (assemblyInfo.CompareTo(_projectOriginalInfos[i]) == 0)
                {
                    i++;
                    continue;
                }
                string assemblyInfoFile = assemblyInfo.Project.GetAssemblyInfoFileName();
                CheckOut(assemblyInfo, assemblyInfoFile);
                assemblyInfo.Project.ModifyAssemblyInfo(assemblyInfo);
                i++;
            }

            toolStripStatusLabel.Text = "Saved successfully.";
            DialogResult = DialogResult.OK;
        }
    }
}