using System.IO.Compression;

namespace RemoteHotkeyCore.ScreenCapture.Helpers.DataCompress;

public class CustomScreenCompressor : IScreenDataCompressor
{
    public byte[] Compress(byte[] data)
    {
        byte[] compressedBytes;

        using (var uncompressedStream = new MemoryStream(data))
        {
            using (var compressedStream = new MemoryStream())
            {
                using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
                {
                    uncompressedStream.CopyTo(compressorStream);
                }

                compressedBytes = compressedStream.ToArray();
            }
        }

        return compressedBytes;
    }
}