using Remote_Hub.ViewModels._Base;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Remote_Hub.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Button _connectButton;

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

        }
    }
}