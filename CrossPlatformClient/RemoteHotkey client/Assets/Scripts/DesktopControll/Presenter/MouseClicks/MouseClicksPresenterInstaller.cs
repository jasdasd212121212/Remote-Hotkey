public class MouseClicksPresenterInstaller : DesktopControllPresenterInstallerBase<MouseClicksPresenter, MouseClicksView>
{
    protected override MouseClicksPresenter GetInstance(MouseClicksView view, DesktopControllModel model)
    {
        return new MouseClicksPresenter(view, model);
    }
}