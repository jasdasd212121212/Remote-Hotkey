using System;
using UnityEngine;

[Serializable]
public class VirtualStorageFile
{
    [SerializeField] private string _name;
    [SerializeField] private string _content;
    
    public string Name => _name;
    public string Content => SimplyBytesTextCryptor.GetDecrypted(_content);

    public VirtualStorageFile(string name, string content)
    {
        _name = name;
        _content = SimplyBytesTextCryptor.GetCrypted(content);
    }
}