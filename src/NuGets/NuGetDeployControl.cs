using System;
using System.Linq;
using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.NuGets
{
    public partial class NuGetDeployControl : UserControl
    {
        private NuGetDeployViewModel _viewModel;
        private NuGetConfig _nuGetConfig;

        public NuGetDeployControl()
        {
            InitializeComponent();
            textBoxSymbolServer.Enabled = false;
            textBoxSymbolServer.TextChanged += (sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(textBoxSymbolServer.Text))
                    textBoxSymbolServer.Enabled = true;
            };
        }

        public NuGetConfig NuGetConfig
        {
            get { return _nuGetConfig; }
            set
            {
                _nuGetConfig = value;

                sourceBox.Items.Clear();
                foreach (var source in _nuGetConfig.Sources)
                {
                    sourceBox.Items.Add(source.Url);
                }
            }
        }

        public NuGetDeployViewModel ViewModel
        {
            get
            {
                if (_viewModel == null)
                    _viewModel = new NuGetDeployViewModel();
                _viewModel.NuGetServer = sourceBox.Text;
                _viewModel.RememberKey = chkRemember.Checked;
                _viewModel.ApiKey = textBoxApiKey.Text;
                _viewModel.V2Login = textBoxLogin.Text;
                return _viewModel;
            }
            set
            {
                _viewModel = value;

                sourceBox.DataBindings.Clear();
                sourceBox.DataBindings.Add("Text", _viewModel, "NuGetServer", true, DataSourceUpdateMode.OnPropertyChanged);
                textBoxApiKey.DataBindings.Clear();
                textBoxApiKey.DataBindings.Add("Text", _viewModel, "ApiKey", true, DataSourceUpdateMode.OnPropertyChanged);

                if (!string.IsNullOrWhiteSpace(_viewModel.SymbolServer))
                {
                    textBoxSymbolServer.Enabled = true;
                }
                textBoxSymbolServer.DataBindings.Clear();
                textBoxSymbolServer.DataBindings.Add("Text", _viewModel, "SymbolServer", true, DataSourceUpdateMode.OnPropertyChanged);

                textBoxLogin.DataBindings.Clear();
                textBoxLogin.DataBindings.Add("Text", _viewModel, "V2Login", true, DataSourceUpdateMode.OnPropertyChanged);

            }
        }

        private void sourceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var url = sourceBox.Text.Trim();
            var source = NuGetConfig.Sources.FirstOrDefault(m => m.Url == url);
            textBoxApiKey.Text = source?.ApiKey ?? string.Empty;
            chkRemember.Checked = textBoxApiKey.Text.Length > 0;
            textBoxLogin.Text = source?.UserName;
            checkBoxNugetLogin.Checked = textBoxLogin.Text.Length > 0;
        }


        private void checkBoxNugetLogin_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;

            textBoxLogin.Visible = check.Checked;
            labelLogin.Visible = check.Checked;
        }

    }

    public class NuGetDeployViewModel
    {
        public string NuGetServer { get; set; }
        public string ApiKey { get; set; }
        public bool RememberKey { get; set; }
        public string SymbolServer { get; set; }
        public string V2Login { get; set; }
    } 
}
