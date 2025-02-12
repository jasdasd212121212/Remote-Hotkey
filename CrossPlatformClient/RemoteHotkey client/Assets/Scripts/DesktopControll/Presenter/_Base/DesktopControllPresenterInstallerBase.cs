using Zenject;

public abstract class DesktopControllPresenterInstallerBase<TPresenter, TView> : MonoInstaller 
    where TView : DesktopControllViewBase
    where TPresenter : DesktopControllPresenterBase<TView>
{
    [Inject] private TView _view;
    [Inject] private DesktopControllModel _model;

    public override void InstallBindings()
    {
        Container.Bind<TPresenter>().FromInstance(GetInstance(_view, _model)).AsSingle().Lazy();
    }

    protected abstract TPresenter GetInstance(TView view, DesktopControllModel model);
}