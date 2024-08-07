using UnityEngine;

public class VirtualFileStorageModel
{
    private IStorageSerializerStrategy _serializerStrategy;
    private VirtualStorageDataTransferObject _storage;

    public IReadOnlyStorageDataTransferObject Storage => _storage;

    public VirtualFileStorageModel(IStorageSerializerStrategy storageSerializerStrategy)
    {
        _serializerStrategy = storageSerializerStrategy;
        _storage = new VirtualStorageDataTransferObject();

        Load();
    }

    ~VirtualFileStorageModel()
    {
        Save();
    }

    public void CreateFile(VirtualStorageFile file)
    {
        _storage.AddFile(file);
        Save();
    }

    public void DeleteFile(VirtualStorageFile file)
    {
        _storage.DeleteFile(file);
        Save();
    }

    public void Load()
    {
        if (_serializerStrategy.Exist(ConfigConstantsHolder.STORAGE_SAVE_KEY) == false)
        {
            Save();
            return;
        }

        _storage = _serializerStrategy.Load(ConfigConstantsHolder.STORAGE_SAVE_KEY);
    }

    private void Save()
    {
        _serializerStrategy.Save(ConfigConstantsHolder.STORAGE_SAVE_KEY, _storage);
    }
}