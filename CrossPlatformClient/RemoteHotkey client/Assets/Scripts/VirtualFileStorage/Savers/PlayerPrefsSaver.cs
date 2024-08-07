using UnityEngine;

public class PlayerPrefsSaver : IStorageSerializerStrategy
{
    public bool Exist(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public VirtualStorageDataTransferObject Load(string key)
    {
        string json = PlayerPrefs.GetString(key);

        return JsonUtility.FromJson<VirtualStorageDataTransferObject>(json);
    }

    public void Save(string key, VirtualStorageDataTransferObject storage)
    {
        string json = JsonUtility.ToJson(storage);  

        PlayerPrefs.SetString(key, json);
    }
}