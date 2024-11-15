using Zenject;

public class DesktopModelInstaller : MonoInstaller
{
    [Inject] private RemoteHotkeyClientModel _client;

    public override void InstallBindings()
    {
        Container.Bind<DesktopModel>().FromInstance(new DesktopModel(_client)).AsSingle().NonLazy();
    }
}