using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkey.Network.Server;
using RemoteHotkeyCore.ScreenCapture.Helpers.CursorDrawer;
using RemoteHotkeyCore.ScreenCapture.Helpers.ScreenSize;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCaptureNetworkSender
{
    private ScreenCapturer _screen;
    private IServer _server;

    public ScreenCaptureNetworkSender(IServer server, MouseController mouse)
    {
        _screen = new ScreenCapturer(mouse, new WinApiLowLevelScreenSizeObtainer(), new FileImageCursorDrawer());
        _server = server;

        CaptureLoop();
    }

    private async void CaptureLoop()
    {
        while (true)
        {
            _server.SendMessageToClient(_screen.CaptureScreen());

            await Task.Delay(50);
        }
    }
}