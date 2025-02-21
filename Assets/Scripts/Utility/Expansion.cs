using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Expansion
{
    public static T GetOrAnyComponent<T>(this Component obj) where T : Component
    {
        if(!obj.TryGetComponent<T>(out var component)) return obj.AddComponent<T>();
        else return component;
    }
}
