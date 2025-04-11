namespace RemoteHotkeyCore.ScreenCapture.Helpers.ScreenSize;

public interface IScreenSizeObtainer
{
    void GetSize(ref int width, ref int height);
}