using System.Drawing;
using System.Drawing.Imaging;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore;
using RemoteHotkeyCore.ScreenCapture.Helpers.CursorDrawer;
using RemoteHotkeyCore.ScreenCapture.Helpers.DataCompress;
using RemoteHotkeyCore.ScreenCapture.Helpers.ScreenSize;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCapturer
{
    private int _width;
    private int _height;

    private int _compressionLevel;

    private IScreenSizeObtainer _screenSizeObtainer;
    private ICursorDrawer _cursorDrawer;
    private IScreenDataCompressor _dataCompressor;

    private ImageCodecInfo _imageCodec;
    private EncoderParameters _codecParametr;

    private MouseController _mouse;
    private Bitmap _screen;

    private readonly ImageFormat IMAGE_FORMAT = ImageFormat.Jpeg;
    private const int IMAGE_COMPRESSION_LEVEL = 55;

    public ScreenCapturer(MouseController mouse, IScreenSizeObtainer screenSizeObtainer, ICursorDrawer cursorDrawer, IScreenDataCompressor dataCompressor)
    {
        _mouse = mouse;
        _compressionLevel = Math.Clamp(new Config().Data.CompressionLevel, 1, int.MaxValue);

        _screenSizeObtainer = screenSizeObtainer;
        _cursorDrawer = cursorDrawer;
        _dataCompressor = dataCompressor;

        _screenSizeObtainer.GetSize(ref _width, ref _height);
        _screen = new Bitmap(_width, _height);

        _imageCodec = GetEncoder(IMAGE_FORMAT);
        _codecParametr = BuildCodec(IMAGE_COMPRESSION_LEVEL);
    }

    ~ScreenCapturer()
    {
        _screen.Dispose();
    }

    public byte[] CaptureScreen()
    {
        Size screenSize = new Size(_width, _height);
        Bitmap frame;

        if (_screen == null || _screen.Width != screenSize.Width || _screen.Height != screenSize.Height)
        {
            _screen = new Bitmap(screenSize.Width, screenSize.Height);
        }

        frame = _screen;

        Graphics graphics = Graphics.FromImage(frame);
        graphics.CopyFromScreen(Point.Empty, Point.Empty, screenSize);
        _cursorDrawer.DrawCursor(GetBiggerSide(), graphics, _mouse.GetMousePosition(), _screen);

        frame = ScaleImage(frame, _width / _compressionLevel, _height / _compressionLevel);

        byte[] pngData;
        using (MemoryStream ms = new MemoryStream())
        {
            frame.Save(ms, _imageCodec, _codecParametr);
            pngData = ms.ToArray();
        }

        graphics.Dispose();

        return _dataCompressor.Compress(pngData);
    }

    public Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        var newImage = new Bitmap(newWidth, newHeight);
        Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
        Bitmap bmp = new Bitmap(newImage);

        return bmp;
    }

    private int GetBiggerSide()
    {
        return Math.Max(_width, _height);
    }

    private EncoderParameters BuildCodec(int quality)
    {
        Encoder encoder = Encoder.Quality;
        EncoderParameters codecParams = new EncoderParameters(1);
        EncoderParameter codec = new EncoderParameter(encoder, quality);

        codecParams.Param[0] = codec;

        return codecParams;
    }

    private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        foreach (ImageCodecInfo info in codecs)
        {
            if (info.FormatID == format.Guid)
            {
                return info;
            }
        }

        return null;
    }
}