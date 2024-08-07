using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public class VirtualStorageDataTransferObject : IReadOnlyStorageDataTransferObject
{
    [SerializeField] private VirtualStorageFile[] _files;

    public VirtualStorageFile[] Files => _files;

    public void AddFile(VirtualStorageFile file)
    {
        List<VirtualStorageFile> files = _files.ToList();
        files.Add(file);

        _files = files.ToArray();
    }

    public void DeleteFile(VirtualStorageFile file)
    {
        List<VirtualStorageFile> files = _files.ToList();

        if (files.Contains(file) == false)
        {
            Debug.LogError("Critical error -> can`t delete not existing file");
            return;
        }

        files.Remove(file);
        _files = files.ToArray();
    }
}