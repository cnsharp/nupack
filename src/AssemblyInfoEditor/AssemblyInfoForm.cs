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
    public partial class AssemblyInfoForm : Form
    {
        private readonly IEnumerable<Project> _refProjects;

        #region Constants and Fields


        //private readonly Project _startProject;
        private readonly List<Project> _projects;
        private readonly List<ProjectAssemblyInfo> _projectInfos;
        private readonly ProjectAssemblyInfo[] _projectOriginalInfos;
        private CommonAssemblyInfo _commonInfo;
        private string _fileToLink;
        private static readonly string[] commonInfoFields =
            typeof(CommonAssemblyInfo).GetProperties().Select(p => p.Name).ToArray();

        #endregion

        #region Constructors and Destructors

        public AssemblyInfoForm(IEnumerable<Project> refProjects)
        {
            InitializeComponent();
            Icon = Resource.logo;

            projectGrid.AutoGenerateColumns = false;
            RegisterEvents();

             _projects = refProjects.ToList();
             _projectInfos = _projects.Select(p => p.GetProjectAssemblyInfo()).ToList();
            _projectOriginalInfos = _projectInfos.Select(p => p.Copy()).ToArray();
            projectBindingSource.DataSource = _projectInfos;
            projectGrid.DataSource = projectBindingSource;

            //pnlCopy.Visible = projects.Count > 1;

        }

        private void RegisterEvents()
        {
            txtCompany.TextChanged += CommonInfoTextChanged;
            txtCopyright.TextChanged += CommonInfoTextChanged;
            txtProduct.TextChanged += CommonInfoTextChanged;
            txtVersion.TextChanged += CommonInfoTextChanged;
        }

        private void CommonInfoTextChanged(object sender, EventArgs e)
        {
            var box = sender as TextBox;
           
            var cellName = box.Name.Replace("txt", "Col");
            foreach (DataGridViewRow row in projectGrid.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    row.Cells[cellName].Value = box.Text.Trim();
                }
            }
            projectGrid.EndEdit();
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

        private void CopyCellValues(int row)
        {
            foreach (var field in commonInfoFields)
            {
                var cellName = "Col" + field;
                var textbox = groupBoxCommonInfo.Controls.Find("txt" + field, false).FirstOrDefault() as TextBox;
                if (textbox != null)
                    projectGrid.Rows[row].Cells[cellName].Value = textbox.Text.Trim();
            }
            projectGrid.EndEdit();
        }


        private void RevertCellValue(int row)
        {
            foreach (var field in commonInfoFields)
            {
                var cellName = "Col" + field;
                var info = _projectOriginalInfos[row];
                var v = typeof(ProjectAssemblyInfo).GetProperty(field).GetValue(info, null);
                projectGrid.Rows[row].Cells[cellName].Value = v;
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
                AssemblyInfoUtil.Save(assemblyInfo);
                i++;
            }

            toolStripStatusLabel.Text = "Saved successfully.";
            DialogResult = DialogResult.OK;
        }

        private void projectGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var field = projectGrid.Columns[e.ColumnIndex].DataPropertyName;
            if (commonInfoFields.Contains(field))
            {
                var textbox = groupBoxCommonInfo.Controls.Find("txt" + field,false).FirstOrDefault() as TextBox;
                var selected = (bool)projectGrid.CurrentRow.Cells[0].Value;
                if (!string.IsNullOrWhiteSpace(textbox?.Text) && selected)
                    e.Cancel = true;
            }
        }

        private void projectGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var selected = (bool) projectGrid.CurrentCell.Value;
                if(selected) CopyCellValues(e.RowIndex);
                else RevertCellValue(e.RowIndex);
            }
        }

        private string DefaultExt => _projects[0].GetCodeFileExtension();

        private void btnNewOrChange_Click(object sender, EventArgs e)
        {
            saveAssemblyInfoFileDialog.DefaultExt = DefaultExt;
            saveAssemblyInfoFileDialog.InitialDirectory = Path.GetDirectoryName(Host.Instance.DTE.Solution.FileName);
            saveAssemblyInfoFileDialog.FileName = typeof(CommonAssemblyInfo).Name + saveAssemblyInfoFileDialog.DefaultExt;
            if(saveAssemblyInfoFileDialog.ShowDialog() != DialogResult.OK) return;
            var info = _projectOriginalInfos[0];
            _commonInfo = new CommonAssemblyInfo
            {
                Company = info.Company,
                Copyright = info.Copyright,
                Product = info.Product,
                Trademark = info.Trademark
            };
            _commonInfo.Save(saveAssemblyInfoFileDialog.FileName);
            _fileToLink = saveAssemblyInfoFileDialog.FileName;
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            openAssemblyInfoFileDialog.DefaultExt = DefaultExt;
            openAssemblyInfoFileDialog.InitialDirectory = Path.GetDirectoryName(Host.Instance.DTE.Solution.FileName);
            if (openAssemblyInfoFileDialog.ShowDialog() != DialogResult.OK) return;
            _commonInfo = AssemblyInfoUtil.ReadCommonAssemblyInfo(openAssemblyInfoFileDialog.FileName);
            _fileToLink = openAssemblyInfoFileDialog.FileName;
        }

        void BindCommonInfo()
        {
            
        }
    }
}