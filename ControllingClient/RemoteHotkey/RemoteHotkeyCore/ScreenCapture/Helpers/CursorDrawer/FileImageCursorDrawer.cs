using RemoteHotkey.InputsConstrollSystem;
using System.Drawing;

namespace RemoteHotkeyCore.ScreenCapture.Helpers.CursorDrawer;

public class FileImageCursorDrawer : ICursorDrawer
{
    private PathFinder _pathFinder;
    private Image _cursor;
    private string _path;

    private const string CURSOR_FILE_NAME = "Cursor.png";
    private const float CURSOR_SIZE_PERCENT = 0.013f;

    public FileImageCursorDrawer()
    {
        _pathFinder = new PathFinder();
        _path = $"{_pathFinder.PathToRoot}\\{CURSOR_FILE_NAME}";

        _cursor = Image.FromFile(_path);
    }

    public void DrawCursor(int biggerSide, Graphics captureGraphics, MouseController.POINT mousePoint, Bitmap screen)
    {
        int cursorSize = (int)((float)((float)biggerSide * CURSOR_SIZE_PERCENT));

        if (mousePoint.x < screen.Width && mousePoint.y < screen.Height)
        {
            captureGraphics.DrawImage(_cursor, mousePoint.x, mousePoint.y, cursorSize, cursorSize);
        }
    }
}