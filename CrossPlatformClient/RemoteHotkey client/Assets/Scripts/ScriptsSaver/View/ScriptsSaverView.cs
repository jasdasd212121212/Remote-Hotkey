using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScriptsSaverView : MonoBehaviour
{
    [SerializeField] private ScriptsSaverPresenter _presenter;

    [Space]

    [SerializeField] private VerticalLayoutGroup _container;
    [SerializeField] private ScriptSavedItemView _viewPrefab;

    [Inject] private ScriptsSaverModel _model;

    private MonoFactory<ScriptSavedItemView> _factory;
    private List<ScriptSavedItemView> _spawnedViews = new List<ScriptSavedItemView>();

    private void Awake()
    {
        _factory = new MonoFactory<ScriptSavedItemView>(_viewPrefab, _container.transform);

        _model.allScriptsLoaded += DisplayAll;

        DisplayAll(_presenter.ScriptNames);
    }

    private void OnDestroy()
    {
        _model.allScriptsLoaded -= DisplayAll;
    }

    private void DisplayAll(string[] names)
    {
        ClearAllSpawned();

        foreach (string name in names)
        {
            ScriptSavedItemView view = _factory.Create();
            view.Initialize(name, _presenter);

            _spawnedViews.Add(view);
        }
    }

    private void ClearAllSpawned()
    {
        ScriptSavedItemView[] views = _spawnedViews.ToArray();

        foreach (ScriptSavedItemView view in views)
        {
            _spawnedViews.Remove(view);
            Destroy(view.gameObject);
        }
    }
}