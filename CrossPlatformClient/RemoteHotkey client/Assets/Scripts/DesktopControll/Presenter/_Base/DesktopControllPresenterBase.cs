public abstract class DesktopControllPresenterBase<TView> where TView : DesktopControllViewBase
{
    protected TView View { get; private set; }
    protected DesktopControllModel Model { get; private set; }

    public DesktopControllPresenterBase(TView view, DesktopControllModel model)
    {
        View = view;
        Model = model;
    }
}