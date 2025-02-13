using Zenject;
using UnityEngine;

public abstract class DesktopControllViewInstallerBase<TView> : NonGenericViewInstallerBase where TView : DesktopControllViewBase
{
    [SerializeField] private ImageInputHelper _desktopViewImage;

    //public override void InstallBindings()
    //{
    //    Container.Bind<TView>().FromInstance(GetInstance(_desktopViewImage)).AsSingle().NonLazy();
    //}

    public override DesktopControllViewBase InstallAndGetView(DiContainer container)
    {
        TView view = GetInstance(_desktopViewImage);
        container.Bind<TView>().FromInstance(GetInstance(_desktopViewImage)).AsSingle().NonLazy();

        return view;
    }

    protected abstract TView GetInstance(ImageInputHelper desktopViewImage);
}