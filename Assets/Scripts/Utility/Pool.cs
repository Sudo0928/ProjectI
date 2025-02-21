using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> where T : class
{
    public ObjectPool<T> ObjectPool { get; private set; }

    public Pool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defualtCapacity = 10, int maxSize = 10000)
    {
        ObjectPool = new ObjectPool<T>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defualtCapacity, maxSize);
    }
}
