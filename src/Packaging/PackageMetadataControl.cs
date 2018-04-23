using System;
using System.ComponentModel;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions.Projects;
using NuGet;

namespace CnSharp.VisualStudio.NuPack.Packaging
{
    public partial class PackageMetadataControl : UserControl
    {
        private ProjectAssemblyInfo _assemblyInfo;
        private ManifestMetadata _manifestMetadata;

        public PackageMetadataControl()
        {
            InitializeComponent();

            ActiveControl = textBoxAssemblyVersion;

            MakeTextBoxRequired(textBoxId);
            MakeTextBoxRequired(textBoxDescription);
            MakeTextBoxRequired(textBoxAuthors);
            MakeTextBoxRequired(textBoxOwners);
            MakeTextBoxRequired(textBoxAssemblyVersion);
            MakeTextBoxRequired(textBoxVersion);
            MakeTextBoxRequired(textBoxFileVersion);
            MakeTextBoxRequired(textBoxReleaseNotes);

            checkBoxRLA.CheckedChanged += (sender, args) =>
            {
                ManifestMetadata.RequireLicenseAcceptance = checkBoxRLA.Checked;
            };

            textBoxAssemblyVersion.TextChanged +=
                (sender, e) =>
                {
                    //textBoxVersion.Text = textBoxFileVersion.Text = textBoxAssemblyVersion.Text.Trim();
                    var ver = textBoxAssemblyVersion.Text.Trim();
                    if(_assemblyInfo != null)
                        _assemblyInfo.FileVersion = ver;
                    textBoxVersion.Text = ver;
                };

            textBoxLicenseUrl.TextChanged += (sender, e) =>
            {
                checkBoxRLA.Enabled = !string.IsNullOrWhiteSpace(textBoxLicenseUrl.Text);
                if (!checkBoxRLA.Enabled && checkBoxRLA.Checked)
                    checkBoxRLA.Checked = false;
            };
        }

        public ProjectAssemblyInfo AssemblyInfo
        {
            get
            {
                return _assemblyInfo;
            }
            set
            {
                _assemblyInfo = value;
                textBoxAssemblyVersion.DataBindings.Clear();
                textBoxAssemblyVersion.DataBindings.Add("Text", _assemblyInfo, "Version", true, DataSourceUpdateMode.OnPropertyChanged);
                textBoxFileVersion.DataBindings.Clear();
                textBoxFileVersion.DataBindings.Add("Text", _assemblyInfo, "FileVersion", true, DataSourceUpdateMode.OnPropertyChanged);
            }
        }

        public ManifestMetadata ManifestMetadata
        {
            get
            {
                return _manifestMetadata;
            }
            set
            {
                _manifestMetadata = value;
                foreach (Control control in Controls)
                {
                    var box = control as TextBox;
                    if (!string.IsNullOrWhiteSpace(box?.Tag?.ToString()))
                    {
                        box.DataBindings.Clear();
                        box.DataBindings.Add("Text", _manifestMetadata, box.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    }
                }
                checkBoxRLA.Checked = _manifestMetadata.RequireLicenseAcceptance;
            }
        }

        public ErrorProvider ErrorProvider { get; set; }
        
        private void MakeTextBoxRequired(TextBox box)
        {
            box.Validating += TextBoxValidating;
            box.Validated += TextBoxValidated;
        }

        private void TextBoxValidated(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            ErrorProvider.SetError(box, null);
        }

        private void TextBoxValidating(object sender, CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                ErrorProvider.SetError(box, "*");
                e.Cancel = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (textBoxDescription.Focused)
                {
                    InsertNewLine(textBoxDescription);
                    return true;
                }
                if (textBoxReleaseNotes.Focused)
                {
                    InsertNewLine(textBoxReleaseNotes);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void InsertNewLine(TextBox box)
        {
            var i = box.SelectionStart;
            box.Text = box.Text.Insert(i, Environment.NewLine);
            box.Select(i + 2, 0);
        }


    }
}
