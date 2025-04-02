using Zenject;

public class CursorStateMachineInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CursorStateMachine>().FromInstance(new CursorStateMachine()).AsSingle().NonLazy();
    }
}