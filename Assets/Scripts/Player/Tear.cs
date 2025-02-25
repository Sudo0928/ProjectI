using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    private SpriteRenderer mainSprite;

    private void Awake()
    {
        mainSprite = GetComponentInChildren<SpriteRenderer>();
    }
}
