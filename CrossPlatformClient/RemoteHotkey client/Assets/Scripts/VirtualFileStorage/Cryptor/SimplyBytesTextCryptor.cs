using System.Text;

public static class SimplyBytesTextCryptor
{
    private static readonly string SEPARATING_STRING = "x";

    public static string GetCrypted(string content)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        string result = "";

        for (int i = 0; i < bytes.Length; i++)
        {
            result += $"{bytes[i]}{SEPARATING_STRING}";
        }

        return result;
    }

    public static string GetDecrypted(string cryptedContent)
    {
        string[] splittedContent = cryptedContent.Split(SEPARATING_STRING);
        byte[] bytes = new byte[splittedContent.Length - 1];

        for (int i = 0; i < (splittedContent.Length - 1); i++)
        {
            byte result = byte.Parse(splittedContent[i]);
            bytes[i] = result;
        }

        return Encoding.UTF8.GetString(bytes);
    }
}