using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using RemoteHotkeyCore;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCapture
{
    private int _width;
    private int _height;

    private int _compressionLevel;

    private Bitmap _screen;

    private const string FILE_NAME = "temp.png";
    private const string COMPRESSED_FILE_NAME = "temp_compressed.png";

    private readonly ImageFormat IMAGE_FORMAT = ImageFormat.Png;

    public ScreenCapture()
    {
        _compressionLevel = Math.Clamp(new Config().Data.CompressionLevel, 1, int.MaxValue);

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

            captureBitmap.Save(FILE_NAME, IMAGE_FORMAT);
        }

        using (Image image = Image.FromFile(FILE_NAME))
        {
            using (Bitmap resizedMap = ResizeImage(image, _width / _compressionLevel, _height / _compressionLevel))
            {
                resizedMap.Save(COMPRESSED_FILE_NAME, IMAGE_FORMAT);
            }
        }

        return File.ReadAllBytes(COMPRESSED_FILE_NAME);
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

    public Bitmap ResizeImage(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.Default;
            graphics.InterpolationMode = InterpolationMode.Default;
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.PixelOffsetMode = PixelOffsetMode.Default;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }
}