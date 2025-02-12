public class MoveMousePresenterInstaller : DesktopControllPresenterInstallerBase<MoveMousePresenter, MoveMouseView>
{
    protected override MoveMousePresenter GetInstance(MoveMouseView view, DesktopControllModel model)
    {
        return new MoveMousePresenter(view, model);
    }
}