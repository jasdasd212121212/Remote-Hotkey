using UnityEngine;
using Zenject;

public abstract class NonGenericPresenterInstallerBase : MonoBehaviour
{
    public abstract void Install(DiContainer container, DesktopControllViewBase view, DesktopControllModel model);
}