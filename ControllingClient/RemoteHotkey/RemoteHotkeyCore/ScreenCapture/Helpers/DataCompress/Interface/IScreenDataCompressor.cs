namespace RemoteHotkeyCore.ScreenCapture.Helpers.DataCompress;

public interface IScreenDataCompressor
{
    byte[] Compress(byte[] data);
}