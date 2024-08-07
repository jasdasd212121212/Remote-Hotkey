using System.IO;
using UnityEngine;

public class LocalFileSaver : IStorageSerializerStrategy
{
    public bool Exist(string key)
    {
        return File.Exists($"{Application.persistentDataPath}/{key}.{ConfigConstantsHolder.STORAGE_FILE_EXTENSION}");
    }

    public VirtualStorageDataTransferObject Load(string key)
    {
        string json = File.ReadAllText($"{Application.persistentDataPath}/{key}.{ConfigConstantsHolder.STORAGE_FILE_EXTENSION}");

        return JsonUtility.FromJson<VirtualStorageDataTransferObject>(json);
    }

    public void Save(string key, VirtualStorageDataTransferObject storage)
    {
        string json = JsonUtility.ToJson(storage);

        File.WriteAllText($"{Application.persistentDataPath}/{key}.{ConfigConstantsHolder.STORAGE_FILE_EXTENSION}", json);
    }
}