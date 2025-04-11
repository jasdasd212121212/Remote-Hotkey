using RemoteHotkey.InputsConstrollSystem;
using System.Drawing;

namespace RemoteHotkeyCore.ScreenCapture.Helpers.CursorDrawer;

public interface ICursorDrawer
{
    void DrawCursor(int biggerSide, Graphics captureGraphics, MouseController.POINT mousePoint, Bitmap screen);
}