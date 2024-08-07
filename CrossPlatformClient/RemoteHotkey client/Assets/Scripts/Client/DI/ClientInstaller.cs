using UnityEngine;
using Zenject;

public class ClientInstaller : MonoInstaller
{
    [SerializeField][Min(100)] private int _reconnectDellay;
    [SerializeField][Min(1)] private int _reconnectsCount;

    private RemoteHotkeyClientModel _client;

    public override void InstallBindings()
    {
        _client = new RemoteHotkeyClientModel(_reconnectDellay, _reconnectsCount);

        Container.Bind<RemoteHotkeyClientModel>().FromInstance(_client).AsSingle().NonLazy();
    }

    private void OnDestroy()
    {
        _client.Dispose();
    }
}