using System;
using UnityEngine;
using Zenject;

public class ScriptsSaverPresenter : MonoBehaviour
{
    [SerializeField] private ClientPresenter _client;

    [Inject] private ScriptsSaverModel _model;

    public string[] ScriptNames => _model.ScriptsNames;

    public void Save(string script)
    {
        _model.SaveScript($"Script - {Guid.NewGuid().ToString().Split('-')[0]}", script);
    }

    public void Load(string name)
    {
        _model.LoadScript(name);
    }

    public void Rename(string currentName, string newName)
    {
        newName = newName.Trim().Replace('"', ' ').Replace(':', ' ');

        string content = VirtualStorageFacade.ReadFile(currentName);
        VirtualStorageFacade.DeleteFile(currentName);

        _model.SaveScript(newName, content);
    }

    public void Delete(string name)
    {
        VirtualStorageFacade.DeleteFile(name);
        _model.Refrash();
    }

    public void Execute(string name)
    {
        _client.SendCustomScript(VirtualStorageFacade.ReadFile(name));
    }
}