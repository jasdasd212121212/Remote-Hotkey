using System.IO.Compression;

namespace RemoteHotkeyCore.ScreenCapture.Helpers.DataCompress;

public class GZipScreenCompressor : IScreenDataCompressor
{
    public byte[] Compress(byte[] data)
    {
        using (var compressedStream = new MemoryStream())
        using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
        {
            zipStream.Write(data, 0, data.Length);
            return compressedStream.ToArray();
        }
    }
}