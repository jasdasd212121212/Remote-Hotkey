using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCapture
{
    private Bitmap _screen;
    private const string FILE_NAME = "temp.jpeg";

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
        Rectangle bounds = new Rectangle(0, 0, 1920, 1080);

        using (Bitmap captureBitmap = new Bitmap(bounds.Width, bounds.Height))
        {
            using (Graphics captureGraphics = Graphics.FromImage(captureBitmap))
            {
                captureGraphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }

            captureBitmap.Save(FILE_NAME, ImageFormat.Jpeg);
        }

        return File.ReadAllBytes(FILE_NAME);
    }
}