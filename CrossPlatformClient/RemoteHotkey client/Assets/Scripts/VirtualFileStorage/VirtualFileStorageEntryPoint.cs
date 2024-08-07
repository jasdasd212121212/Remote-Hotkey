using UnityEngine;

public class VirtualFileStorageEntryPoint
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Construct()
    {
        IStorageSerializerStrategy storageSerializerStrategy = new LocalFileSaver();

        VirtualStorageFacade.Initialize(storageSerializerStrategy);
    }
}