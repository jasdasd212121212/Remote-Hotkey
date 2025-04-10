using RemoteHotkey.InputsConstrollSystem;
using RemoteHotkey.Network.Server;

namespace RemoteHotkey.ScreenCapture;

public class ScreenCaptureNetworkSender
{
    private ScreenCapture _screen;
    private IServer _server;

    public ScreenCaptureNetworkSender(IServer server, MouseController mouse)
    {
        _screen = new ScreenCapture(mouse);
        _server = server;

        CaptureLoop();
    }

    private async void CaptureLoop()
    {
        while (true)
        {
            _server.SendMessageToClient(_screen.CaptureScreen());

            await Task.Delay(55);
        }
    }
}