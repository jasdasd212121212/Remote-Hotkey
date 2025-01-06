using System.Text;

namespace RemoteHotkeyCore.InputsController.Helpers;

public class KyeboardDataFormatter
{
    private const string SEPARATOR = "|:|";

    public KeyboardDataResponse DecodeBytes(byte[] data)
    {
        string raw = Encoding.ASCII.GetString(data);
        byte operationByte = data[data.Length - 1];

        string key = raw.Split(SEPARATOR)[0];
        KeyboardOperationCode code = operationByte == 1 ? KeyboardOperationCode.KeyPressed : KeyboardOperationCode.KeyReleased;

        return new KeyboardDataResponse(code, key);
    }
}