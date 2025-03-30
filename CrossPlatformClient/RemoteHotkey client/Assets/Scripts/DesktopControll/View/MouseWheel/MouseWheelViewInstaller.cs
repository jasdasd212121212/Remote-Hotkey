using UnityEngine;

public class MouseWheelViewInstaller : DesktopControllViewInstallerBase<MouseWheelView>
{
    [SerializeField][Min(0.000001f)] private float _updateDelay;
    [SerializeField][Min(0.001f)] private float _sendDelay;

    protected override MouseWheelView GetInstance(ImageInputHelper desktopViewImage)
    {
        return new MouseWheelView(desktopViewImage, _updateDelay, _sendDelay);
    }
}