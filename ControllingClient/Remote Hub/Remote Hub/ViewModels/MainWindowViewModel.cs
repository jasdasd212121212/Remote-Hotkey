using Remote_Hub.Models.Configs;
using Remote_Hub.Models.ModulesBoot;
using Remote_Hub.ViewModels._Base;
using Remote_Hub.Models.Configs.Data;
using System.Windows;
using System.Windows.Input;

namespace Remote_Hub.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Button _connectButton;

        private Config _configModel;
        private ModulesBootModel _modules;

        public string Ip
        {
            get { return (string)GetValue(IpProperty); }
            set { SetValue(IpProperty, value); }
        }

        public static readonly DependencyProperty IpProperty =
            DependencyProperty.Register("Ip", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(""));

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(""));

        public int CompressionLevel
        {
            get { return (int)GetValue(CompressionLevelProperty); }
            set { SetValue(CompressionLevelProperty, value); }
        }

        public static readonly DependencyProperty CompressionLevelProperty =
            DependencyProperty.Register("CompressionLevel", typeof(int), typeof(MainWindowViewModel), new PropertyMetadata(1));

        public bool UseKeyboardModule
        {
            get { return (bool)GetValue(UseKeyboardModuleProperty); }
            set { SetValue(UseKeyboardModuleProperty, value); }
        }

        public static readonly DependencyProperty UseKeyboardModuleProperty =
            DependencyProperty.Register("UseKeyboardModule", typeof(bool), typeof(MainWindowViewModel), new PropertyMetadata(false));

        public int ValidCompressionLevel { get; private set; }

        public ICommand ConnectButton
        {
            get
            {
                if (_connectButton == null)
                {
                    _connectButton = new Button();
                }

                return _connectButton;
            }
        }

        protected override void OnInit()
        {
            _configModel = new Config();
            _modules = new ModulesBootModel();

            ConfigData currentConfig = _configModel.ReadConfig();

            Ip = currentConfig.Ip;
            Username = currentConfig.UserName;
            CompressionLevel = currentConfig.CompressionLevel;

            _connectButton.clicked += Boot;
        }

        ~MainWindowViewModel()
        {
            if (_connectButton != null)
            {
                _connectButton.clicked -= Boot;
            }
        }

        private void Boot()
        {
            WriteConfig();
            BootModules();
        }

        private void WriteConfig()
        {
            _configModel.WriteConfig(new ConfigData(Ip, Username, CompressionLevel));
        }

        private void BootModules()
        {
            _modules.StartClient();

            if (UseKeyboardModule)
            {
                _modules.StartKeyboardModule();
            }
        }
    }
}