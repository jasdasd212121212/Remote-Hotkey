public interface IStorageSerializerStrategy
{
    bool Exist(string key);
    void Save(string key, VirtualStorageDataTransferObject storage);
    VirtualStorageDataTransferObject Load(string key);
}