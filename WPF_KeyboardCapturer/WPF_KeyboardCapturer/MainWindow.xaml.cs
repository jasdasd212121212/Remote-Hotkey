using System.Windows;

namespace WPF_KeyboardCapturer
{
    public partial class MainWindow : Window
    {
        private KeyboardHooks _hooks;
        private CaptureModel _captureModel;

        public MainWindow()
        {
            InitializeComponent();

            _hooks = new KeyboardHooks();

            Sender sender = new Sender();
            _captureModel = new CaptureModel(new KeysAdapter(sender));

            _hooks.KeyDown += OnKeyDown;
            _hooks.KeyUp += OnKeyUp;

            sender.sended += OnSend;
        }

        private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _captureModel.OnKeyUp(sender, e);
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _captureModel.OnKeyDown(sender, e);
        }

        private void OnSend(string key, bool isDown)
        {
            Debug.Content = $"Last key: {key}; IsDown: {isDown}";
        }
    }
}