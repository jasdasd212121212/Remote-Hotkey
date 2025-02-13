public class MouseClicksViewInstaller : DesktopControllViewInstallerBase<MouseClicksView>
{
    protected override MouseClicksView GetInstance(ImageInputHelper desktopViewImage)
    {
        return new MouseClicksView(desktopViewImage);
    }
}