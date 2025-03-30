public class KeyboardViewInstaller : DesktopControllViewInstallerBase<KeyboardView>
{
    protected override KeyboardView GetInstance(ImageInputHelper desktopViewImage)
    {
        return new KeyboardView(desktopViewImage);
    }
}