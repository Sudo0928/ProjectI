using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    private GameManager gameManager;

    // key = id
    public Dictionary<int, ObjectPool<GameObject>> pools = new Dictionary<int, ObjectPool<GameObject>>();

    internal void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public T GetObject<T>(int id) where T : Component
    {
        if (pools.TryGetValue(id, out var pool))
        {
            GameObject gameObject = pool.Get();
            if (gameObject.TryGetComponent<T>(out var component))
            {
                return component;
            }
            else
            {
                pool.Release(gameObject);
                throw new Exception("This object has no corresponding " + typeof(T).ToString() + " component.");
            }
        }
        else throw new Exception("That ID does not exist.");
    }

    //private void CreatePool(int id)
    //{
    //    ObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defualtCapacity, maxSize);
    //}

    //private GameObject CreateFunc()
    //{
    //    return a;
    //}

    //public void RegisterPool(Func<GameObject> createFunc, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null, Action<GameObject> actionOnDestroy = null, bool collectionCheck = true, int defualtCapacity = 10, int maxSize = 10000)
    //{
    //    ObjectPool<GameObject> pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defualtCapacity, maxSize);
    //}
}
