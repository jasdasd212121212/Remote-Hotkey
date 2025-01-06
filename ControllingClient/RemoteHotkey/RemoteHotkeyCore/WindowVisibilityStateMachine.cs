using System.Runtime.InteropServices;

namespace RemoteHotkey.LowLevel;

public class WindowVisibilityStateMachine
{
    [DllImport("Kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("User32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

    public void SetVisibilityState(bool isVisibility)
    {
        IntPtr hWnd = GetConsoleWindow();

        if (hWnd != IntPtr.Zero)
        {
            if (isVisibility == true)
            {
                ShowWindow(hWnd, 5);
            }
            else
            {
                ShowWindow(hWnd, 0);
            }
        }

        Marshal.FreeHGlobal(hWnd);
    }

    public async void SetVisibilityState(bool isVisibility, float dellay)
    {
        await Task.Delay(TimeSpan.FromSeconds(dellay));
        SetVisibilityState(isVisibility);
    }
}