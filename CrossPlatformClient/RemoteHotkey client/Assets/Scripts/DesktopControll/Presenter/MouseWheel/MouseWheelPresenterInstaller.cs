public class MouseWheelPresenterInstaller : DesktopControllPresenterInstallerBase<MouseWheelPresenter, MouseWheelView>
{
    protected override MouseWheelPresenter GetInstance(MouseWheelView view, DesktopControllModel model)
    {
        return new MouseWheelPresenter(view, model);
    }
}