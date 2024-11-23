using UnityEngine;

public class PackegeParserHelper : MonoBehaviour
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