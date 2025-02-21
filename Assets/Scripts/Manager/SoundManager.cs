using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private GameManager gameManager;

    internal void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
