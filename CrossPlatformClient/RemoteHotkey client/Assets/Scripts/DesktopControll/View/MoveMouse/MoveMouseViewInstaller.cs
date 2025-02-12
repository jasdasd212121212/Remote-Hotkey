public class MoveMouseViewInstaller : DesktopControllViewInstallerBase<MoveMouseView>
{
    protected override MoveMouseView GetInstance(ImageInputHelper desktopViewImage)
    {
        return new MoveMouseView(desktopViewImage);
    }
}