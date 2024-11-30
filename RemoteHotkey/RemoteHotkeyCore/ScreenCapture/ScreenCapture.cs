using System.Drawing;
using System.Drawing.Imaging;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCapture
{
    private int _width;
    private int _height;

    private Bitmap _screen;
    private const string FILE_NAME = "temp.jpeg";

    public ScreenCapture()
    {
        GetScreen(ref _width, ref _height);
        _screen = new Bitmap(_width, _height);
    }

    ~ScreenCapture()
    {
        _screen.Dispose();
    }

    public byte[] CaptureScreen()
    {
        Rectangle bounds = new Rectangle(0, 0, _width, _height);

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

    private void GetScreen(ref int width, ref int height)
    {
        var managementScope = new System.Management.ManagementScope();
        managementScope.Connect();
        var q = new System.Management.ObjectQuery("SELECT CurrentHorizontalResolution, CurrentVerticalResolution FROM Win32_VideoController");
        var searcher = new System.Management.ManagementObjectSearcher(managementScope, q);
        var records = searcher.Get();

        int rr = records.Count;

        foreach (var record in records)
        {
            if (!int.TryParse(record.GetPropertyValue("CurrentHorizontalResolution").ToString(), out width))
            {
                throw new Exception("Throw some exception");
            }
            if (!int.TryParse(record.GetPropertyValue("CurrentVerticalResolution").ToString(), out height))
            {
                throw new Exception("Throw some exception");
            }
        }
    }
}