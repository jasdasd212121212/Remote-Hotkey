using Zenject;
using UnityEngine;

public abstract class DesktopControllViewInstallerBase<TView> : MonoInstaller where TView : DesktopControllViewBase
{
    [SerializeField] private ImageInputHelper _desktopViewImage;

    public override void InstallBindings()
    {
        Container.Bind<TView>().FromInstance(GetInstance(_desktopViewImage)).AsSingle().NonLazy();
    }

    protected abstract TView GetInstance(ImageInputHelper desktopViewImage);
}