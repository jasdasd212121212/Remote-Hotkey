public class KeyboardPresenterInstaller : DesktopControllPresenterInstallerBase<KeyboardPresenter, KeyboardView>
{
    protected override KeyboardPresenter GetInstance(KeyboardView view, DesktopControllModel model)
    {
        return new KeyboardPresenter(view, model);
    }
}