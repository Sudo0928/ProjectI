using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static T Instance;
	public static T instance
	{
		get
		{
			if (Instance == null)
			{
				Instance = FindFirstObjectByType<T>();
				if (Instance == null)
				{
					GameObject go = new GameObject(typeof(T).Name);
					Instance = go.AddComponent<T>();
					DontDestroyOnLoad(go);
				}
			}
			return Instance;

		}
	}

	protected virtual void Awake()
	{
		if (Instance == null)
		{
			Instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
}
