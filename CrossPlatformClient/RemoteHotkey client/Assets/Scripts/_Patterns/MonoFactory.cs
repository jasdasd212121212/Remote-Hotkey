using UnityEngine;

public class MonoFactory<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _parent;

    public MonoFactory() { }

    public MonoFactory(T prefab)
    {
        _prefab = prefab;
    }

    public MonoFactory(T prefab, Transform parent) : this(prefab)
    {
        _parent = parent;
    }

    public T Create()
    {
        return Create(_prefab);
    }

    public T Create(T prefab)
    {
        return GameObject.Instantiate(prefab, _parent);
    }

    public T Create(Vector3 position, Quaternion rotation)
    {
        return Create(_prefab, position, rotation);
    }

    public T Create(T prefab, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(prefab, position, rotation);
    }
}