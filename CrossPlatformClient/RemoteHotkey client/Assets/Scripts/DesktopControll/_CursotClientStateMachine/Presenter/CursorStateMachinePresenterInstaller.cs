using Zenject;

public class CursorStateMachinePresenterInstaller : MonoInstaller
{
    [Inject] private CursorStateMachine _model;

    public override void InstallBindings()
    {
        Container.Bind<CursorStateMachinePresenter>().FromInstance(new CursorStateMachinePresenter(_model)).AsSingle().NonLazy();
    }
}