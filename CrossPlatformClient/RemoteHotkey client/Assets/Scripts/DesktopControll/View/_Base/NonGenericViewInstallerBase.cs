using UnityEngine;
using Zenject;

public abstract class NonGenericViewInstallerBase : MonoBehaviour
{
    public abstract DesktopControllViewBase InstallAndGetView(DiContainer container);
}