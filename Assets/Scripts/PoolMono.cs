
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    private T _prefab;
    private T[] _prefabs;
    private Transform _container;
    private List<T> _pools;

    public bool AutoExpand { get; set; }

    public PoolMono(T prefab, int count, Transform container)
    {
        _prefab = prefab;
        _container = container;

        CreatePoolIdenticalObject(count);
    }

    public PoolMono(T[] prefabs, int count, Transform container)
    {
        _prefabs = prefabs;
        _container = container;

        CreatePoolDifferentObject(count);
    }

    private void CreatePoolDifferentObject(int count)
    {
        _pools = new List<T>();

        for (int i = 0; i < _prefabs.Length; i++)
            for (int j = 0; j < count; j++)
                CreateObject(_prefabs[i]);
    }

    private void CreatePoolIdenticalObject(int count)
    {
        _pools = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(T prefab, bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pools.Add(createdObject);
        return createdObject;
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pools.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in _pools)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (AutoExpand && _prefab != null)
            return CreateObject(true);
        else
            return CreateObject(_prefabs[Random.Range(0, _prefabs.Length)]);

        throw new System.Exception($"There is no free elements in pool of type {typeof(T)}");
    }
}
