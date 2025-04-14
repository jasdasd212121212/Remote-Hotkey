using System.Drawing;
using System.Drawing.Imaging;
using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkeyCore;
using RemoteHotkeyCore.ScreenCapture.Helpers.CursorDrawer;
using RemoteHotkeyCore.ScreenCapture.Helpers.ScreenSize;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCapturer
{
    private int _width;
    private int _height;

    private int _compressionLevel;

    private IScreenSizeObtainer _screenSizeObtainer;
    private ICursorDrawer _cursorDrawer;

    private MouseController _mouse;
    private Bitmap _screen;

    public ScreenCapturer(MouseController mouse, IScreenSizeObtainer screenSizeObtainer, ICursorDrawer cursorDrawer)
    {
        _mouse = mouse;
        _compressionLevel = Math.Clamp(new Config().Data.CompressionLevel, 1, int.MaxValue);

        _screenSizeObtainer = screenSizeObtainer;
        _cursorDrawer = cursorDrawer;

        _screenSizeObtainer.GetSize(ref _width, ref _height);
        _screen = new Bitmap(_width, _height);
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
            frame.Save(ms, ImageFormat.Png);
            pngData = ms.ToArray();
        }

        graphics.Dispose();

        return pngData;
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
}