using System.Text;

namespace RemoteHotkey.Network.Server;

public class ExecuteScriptHandleStep : IPackegeHandlerStep<string>
{
    public Action<string> Callback { get; private set; }

    public ExecuteScriptHandleStep(Action<string> callback)
    {
        Callback = callback;
    }

    public void HandlePackege(byte[] data)
    {
        if (data[0] == 1)
        {
            byte[] packegeContent = new byte[data.Length - 1];

            for (int i = 1; i < data.Length; i++)
            {
                packegeContent[i - 1] = data[i];
            }

            Callback?.Invoke(Encoding.ASCII.GetString(packegeContent));
        }
    }
}