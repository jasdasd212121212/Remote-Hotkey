using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCapture
{
    private Bitmap _screen;

    public ScreenCapture()
    {
        _screen = new Bitmap(1920, 1080);
    }

    ~ScreenCapture()
    {
        _screen.Dispose();
    }

    public byte[] CaptureScreen()
    {
        Rectangle rectangle = new Rectangle(0, 0, 1920, 1080);

        using (Graphics captureGraphics = Graphics.FromImage(_screen))
        {
            captureGraphics.CopyFromScreen(0, 0, 0, 0, new Size(1920, 1080));
        }

        return ImageToByte(_screen);
    }

    private byte[] ImageToByte(Image img)
    {
        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(img, typeof(byte[]));
    }
}