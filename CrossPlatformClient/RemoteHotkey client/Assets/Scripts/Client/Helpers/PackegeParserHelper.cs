using System.Linq;
using System.Text;
using UnityEngine;

public class PackegeParserHelper
{
    private readonly char USER_NAME_SEPARATE_CHAR;

    public PackegeParserHelper(char clientMessageSeparateChar)
    {
        USER_NAME_SEPARATE_CHAR = clientMessageSeparateChar;
    }

    public byte[] GetMessage(byte[] raw)
    {
        int startIndex = GetStartIndex(raw);
        byte[] result = new byte[raw.Length - startIndex];

        for (int i = startIndex; i < raw.Length; i++)
        {
            result[i - startIndex] = raw[i];
        }

        return result;
    }

    public byte[] ConstructMessage(byte packegeMarkCode, byte[] message, string sourceUserName)
    {
        byte[] userName = Encoding.ASCII.GetBytes(sourceUserName);
        byte[] data = new byte[1] { packegeMarkCode }.Concat(userName).Concat(new byte[1] { (byte)ClientConstantsHolder.USER_NAME_SEPARATE_CHAR }).Concat(message).ToArray();

        return data;
    }

    private int GetStartIndex(byte[] message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i] == USER_NAME_SEPARATE_CHAR)
            {
                return i + 1;
            }
        }

        Debug.LogError($"Critical error -> invalid message error. Message are not contains: {nameof(USER_NAME_SEPARATE_CHAR)} = '{USER_NAME_SEPARATE_CHAR}'");
        return 0;
    }
}