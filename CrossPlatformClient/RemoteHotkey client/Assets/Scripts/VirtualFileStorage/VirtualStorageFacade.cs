using UnityEngine;

public static class VirtualStorageFacade
{
    private static VirtualFileStorageModel _model;

    private static bool _initialized = false;

    public static void Initialize(IStorageSerializerStrategy storageSerializerStrategy)
    {
        if (_initialized == true)
        {
            return;
        }

        _model = new VirtualFileStorageModel(storageSerializerStrategy);
        Debug.Log($"Virtual file storage initialized; Saving strategy: {storageSerializerStrategy}");

        _initialized = true;
    }

    public static void CreateFile(string name, string content)
    {
        if (ExistFile(name) == true)
        {
            WriteFile(name, content);
        }
        else
        {
            _model.CreateFile(new VirtualStorageFile(name, content));
        }
    }

    public static void DeleteFile(string name)
    {
        VirtualStorageFile file = GetFile(name);

        if (file == null)
        {
            Debug.LogError($"Critical error -> file by name: {name} are not exist");
            return;
        }

        _model.DeleteFile(file);
    }

    public static void WriteFile(string name, string content)
    {
        if (ExistFile(name) == false)
        {
            return;
        }

        DeleteFile(name);
        CreateFile(name, content);
    }

    public static string ReadFile(string name)
    {
        VirtualStorageFile file = GetFile(name);

        if (file == null)
        {
            Debug.LogError($"Critical error -> file by name: {name} are not exist");
            return "";
        }

        return file.Content;
    }

    public static string[] GetAllFileNames()
    {
        _model.Load();

        string[] result = new string[_model.Storage.Files.Length];

        for (int i = 0; i < _model.Storage.Files.Length; i++)
        {
            result[i] = _model.Storage.Files[i].Name;
        }

        return result;
    }

    public static bool ExistFile(string name)
    {
        return GetFile(name) != null;
    }

    private static VirtualStorageFile GetFile(string name)
    {
        for (int i = 0; i < _model.Storage.Files.Length; i++)
        {
            if (_model.Storage.Files[i].Name == name)
            {
                return _model.Storage.Files[i];
            }
        }

        return null;
    }
}