using Zenject;

public class ScriptSaverInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ScriptsSaverModel>().FromInstance(new ScriptsSaverModel()).AsSingle().NonLazy();
    }
}