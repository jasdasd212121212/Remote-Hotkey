using Zenject;

public abstract class DesktopControllPresenterInstallerBase<TPresenter, TView> : NonGenericPresenterInstallerBase 
    where TView : DesktopControllViewBase
    where TPresenter : DesktopControllPresenterBase<TView>
{
    public override void Install(DiContainer container, DesktopControllViewBase view, DesktopControllModel model)
    {
        container.Bind<TPresenter>().FromInstance(GetInstance(view as TView, model)).AsSingle().Lazy();
    }

    protected abstract TPresenter GetInstance(TView view, DesktopControllModel model);
}