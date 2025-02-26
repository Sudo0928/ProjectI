using System;
using UnityEngine;
using static DesignEnums;

[Serializable]
public class Stat
{
    [field: SerializeField] private float[] options = new float[Enum.GetValues(typeof(Option)).Length];

    public Stat()
    {
        options = new float[Enum.GetValues(typeof(Option)).Length];
        options[(int)Option.Attack] = 1.0f;
        options[(int)Option.AttackSpeed] = 1.0f;
        options[(int)Option.AttackScale] = 1.0f;
        options[(int)Option.Range] = 1.0f;
        options[(int)Option.RangeScale] = 1.0f;
        options[(int)Option.ProjectileSize] = 1.0f;
        options[(int)Option.ProjectileSpeed] = 1.0f;
        options[(int)Option.Heart] = 1.0f;
        options[(int)Option.Speed] = 3.0f;
    }
}