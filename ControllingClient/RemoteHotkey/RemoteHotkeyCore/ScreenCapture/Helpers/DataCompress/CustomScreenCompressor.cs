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
                // setting the leaveOpen parameter to true to ensure that compressedStream will not be closed when compressorStream is disposed
                // this allows compressorStream to close and flush its buffers to compressedStream and guarantees that compressedStream.ToArray() can be called afterward
                // although MSDN documentation states that ToArray() can be called on a closed MemoryStream, I don't want to rely on that very odd behavior should it ever change
                using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
                {
                    uncompressedStream.CopyTo(compressorStream);
                }

                // call compressedStream.ToArray() after the enclosing DeflateStream has closed and flushed its buffer to compressedStream
                compressedBytes = compressedStream.ToArray();
            }
        }

        return compressedBytes;
    }
}