using UnityEngine;
using Zenject;

public class ClientInstaller : MonoInstaller
{
    [SerializeField][Min(100)] private int _reconnectDellay;
    [SerializeField][Min(1)] private int _reconnectsCount;
    [SerializeField][Min(1000)] private int _graphicsBuffer = 200000;
    [SerializeField][Min(0.01f)] private float _listenReconnectDellay = 5f;

    private IClient _client;

    public override void InstallBindings()
    {
        _client = new JsClientModel();

        Container.Bind<IClient>().FromInstance(_client).AsSingle().NonLazy();
    }

    private void OnDestroy()
    {
        _client.Dispose();
    }
}