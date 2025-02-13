using UnityEngine;
using Zenject;

public class DesktopControllSystemInstaller : MonoInstaller
{
    [SerializeField] private NonGenericViewInstallerBase _view;
    [SerializeField] private NonGenericPresenterInstallerBase _presenter;

    [Inject] private DesktopControllModel _model;

    public override void InstallBindings()
    {
        DesktopControllViewBase view = _view.InstallAndGetView(Container);
        _presenter.Install(Container, view, _model);
    }
}