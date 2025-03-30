using UnityEngine;
using Zenject;

public class DesktopControllModelInstaller : MonoInstaller
{
    [SerializeField] private ClientPresenter _client;

    public override void InstallBindings()
    {
        Container.Bind<DesktopControllModel>().FromInstance(new DesktopControllModel
                (
                    _client, 
                    new MoveMouseCommand(),
                    new ClickMouseButtonCommand(),
                    new RotateMouseWheelCommand(),
                    new PressKeyKeyboardCommand()
                )
            ).AsSingle().NonLazy();
    }
}