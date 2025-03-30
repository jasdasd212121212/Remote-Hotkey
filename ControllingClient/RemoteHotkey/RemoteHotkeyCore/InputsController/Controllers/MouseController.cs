using RemoteHotkeyCore.InputsController.Controllers;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RemoteHotkey.InputsConstrollSystem;

public class MouseController
{
    private bool _mouseLocked;

    private const int MOUSE_MOVE = 0x0001;

    private const int MOUSE_LEFT_DOWN = 0x0002;
    private const int MOUSE_LEFT_UP = 0x0004;

    private const int MOUSE_RIGHT_DOWN = 0x0008;
    private const int MOUSE_RIGHT_UP = 0x0010;

    private const int MOUSE_MIDDLE_DOWN = 0x0020;
    private const int MOUSE_MIDDLE_UP = 0x0040;

    private const uint MOUSEEVENTF_WHEEL = 0x0800;
    private const int MOUSE_ABSOLUTE = 0x8000;

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

    [DllImport("User32.dll", EntryPoint = "GetSystemMetrics", CallingConvention = CallingConvention.Winapi)]
    internal static extern int InternalGetSystemMetrics(int value);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    public void SetMousePosition(Vector2 position, bool isAbsolute)
    {
        Vector2 screenSize = new Vector2(InternalGetSystemMetrics(0), InternalGetSystemMetrics(1));

        if (isAbsolute == true)
        {
            mouse_event(MOUSE_MOVE | MOUSE_ABSOLUTE, (int)Math.Round(position.X * 65536 / screenSize.X), (int)Math.Round(position.Y * 65536 / screenSize.Y), 0, 0);
        }
        else
        {
            mouse_event(MOUSE_MOVE, (int)position.X, (int)position.Y, 0, 0);
        }
    }

    public async void Lock(int lockTics)
    {
        _mouseLocked = true;

        for (int i = 0; i < lockTics; i++)
        {
            SetMousePosition(new Vector2(-2000, -2000), true);

            if (_mouseLocked == false)
            {
                return;
            }

            await Task.Delay(100);
        }
    }

    public void Unlock()
    {
        _mouseLocked = false;
    }

    public void Click(MouseButtonsEnum button, MouseActionEnum action)
    {
        uint down, up;

        switch (button)
        {
            case MouseButtonsEnum.Left:
                up = MOUSE_LEFT_UP;
                down = MOUSE_LEFT_DOWN;
                break;

            case MouseButtonsEnum.Right:
                up = MOUSE_RIGHT_UP;
                down = MOUSE_RIGHT_DOWN;
                break;

            case MouseButtonsEnum.Middle:
                up = MOUSE_MIDDLE_UP;
                down = MOUSE_MIDDLE_DOWN;
                break;

            default:
                throw new NotSupportedException($"{nameof(MouseButtonsEnum)} type: {button} are not supported");
        }

        if (action == MouseActionEnum.Hold || action == MouseActionEnum.Click)
        {
            mouse_event(down, 0, 0, 0, 0);
        }
       
        if (action == MouseActionEnum.Release || action == MouseActionEnum.Click)
        {
            mouse_event(up, 0, 0, 0, 0);
        }
    }

    public void ScrollMouseWheel(int amount)
    {
        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)amount, 0);
    }

    public POINT GetMousePosition()
    {
        POINT pt;
        GetCursorPos(out pt);
        return pt;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}