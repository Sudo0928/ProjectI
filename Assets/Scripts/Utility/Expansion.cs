using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Expansion
{
    public static T GetOrAnyComponent<T>(this Component component) where T : Component
    {
        if(!component.TryGetComponent<T>(out var temp))
        {
            temp = component.AddComponent<T>();
            return temp;
        }
        else return temp;
    }
}
