using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                _instance = gameObject.AddComponent<GameManager>();
                return _instance;
            }
            else return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Init();
    }

    public PoolManager PoolManager { get; private set; }
    public SoundManager SoundManager { get; private set; }
    public DataManager DataManager { get; private set; }

    private void Init()
    {
        PoolManager = GetComponentInChildren<PoolManager>();
        PoolManager.Init(this);

        SoundManager = GetComponentInChildren<SoundManager>();
        SoundManager.Init(this);

        DataManager = new DataManager();
    }
}
