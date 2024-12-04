using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_KeyboardCapturer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Sender sender = new Sender();
            new CaptureModel(new KeysAdapter(sender));

            sender.sended += OnSend;
        }

        private void OnSend(string key, bool isDown)
        {
            Debug.Content = $"Last key: {key}; IsDown: {isDown}";
        }
    }
}