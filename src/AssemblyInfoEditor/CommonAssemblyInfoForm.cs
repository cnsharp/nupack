using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.AssemblyInfoEditor
{
    public partial class CommonAssemblyInfoForm : Form
    {
        private readonly List<ProjectAssemblyInfo> _projectInfos;
        

        public CommonAssemblyInfoForm(IEnumerable<Project> allProjects, Project startProject)
        {
           
            InitializeComponent();
            Icon = Resource.logo;

            List<Project> projects = allProjects.ToList();
            _projectInfos = new List<ProjectAssemblyInfo>();
            foreach (var project in projects)
            {
                var info = project.GetProjectAssemblyInfo();
                info.Project = project;
                _projectInfos.Add(info);
            }
          
            toolStripStatusLabel.Text = string.Format("{0} projects", projects.Count);

            Project refProject = startProject ?? projects[0];
            ProjectAssemblyInfo assemblyInfo = refProject.GetProjectAssemblyInfo();
            txtProduct.Text = assemblyInfo.Product;
            txtCompany.Text = assemblyInfo.Company;
            txtCopyright.Text = assemblyInfo.Copyright;
        }

        private void txtCompany_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;
            errorProvider.SetError(tb, null);
            if (tb.Text.Trim().Length == 0)
            {
                errorProvider.SetError(tb, "*");
                e.Cancel = true;
            }
        }

        private void txtCompany_Validated(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            errorProvider.SetError(tb, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Saving...";
            foreach (ProjectAssemblyInfo assemblyInfo in _projectInfos)
            {
                assemblyInfo.Product = txtProduct.Text;
                assemblyInfo.Company = txtCompany.Text;
                assemblyInfo.Copyright = txtCopyright.Text;

                AssemblyInfoUtil.Save(assemblyInfo);
            }
            DialogResult = DialogResult.OK;
        }
    }
}