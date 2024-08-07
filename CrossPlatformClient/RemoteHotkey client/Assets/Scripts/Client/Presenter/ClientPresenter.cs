using UnityEngine;
using Zenject;
using System.Text;

public class ClientPresenter : MonoBehaviour
{
    [Inject] private RemoteHotkeyClientModel _client;

    public void SendInternalCommand(byte commandId)
    {
        _client.SendData(0, new byte[] { commandId });
    }

    public void SendCustomScript(string script)
    {
        _client.SendData(1, Encoding.ASCII.GetBytes(script));
    }

    public void SendServerWindowActivity(bool activity)
    {
        _client.SendData(2, new byte[] { (byte)(activity == true ? 1 : 0) });
    }
}