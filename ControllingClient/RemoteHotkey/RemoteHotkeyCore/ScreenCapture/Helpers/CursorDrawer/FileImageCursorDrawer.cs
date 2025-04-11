using RemoteHotkey.InputsConstrollSystem;
using System.Drawing;

namespace RemoteHotkeyCore.ScreenCapture.Helpers.CursorDrawer;

public class FileImageCursorDrawer : ICursorDrawer
{
    private PathFinder _pathFinder;
    private string _path;

    private const string CURSOR_FILE_NAME = "Cursor.png";
    private const float CURSOR_SIZE_PERCENT = 0.013f;

    public FileImageCursorDrawer()
    {
        _pathFinder = new PathFinder();
        _path = $"{_pathFinder.PathToRoot}\\{CURSOR_FILE_NAME}";
    }

    public void DrawCursor(int biggerSide, Graphics captureGraphics, MouseController.POINT mousePoint, Bitmap screen)
    {
        int cursorSize = (int)((float)((float)biggerSide * CURSOR_SIZE_PERCENT));

        if (mousePoint.x < screen.Width && mousePoint.y < screen.Height)
        {
            captureGraphics.DrawImage(Image.FromFile(_path), mousePoint.x, mousePoint.y, cursorSize, cursorSize);
        }
    }
}