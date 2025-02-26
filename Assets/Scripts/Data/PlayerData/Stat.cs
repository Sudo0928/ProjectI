using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using static DesignEnums;

[Serializable]
public class Stat
{
    [field: SerializeField] private float[] options = new float[Enum.GetValues(typeof(Option)).Length];

    public Dictionary<Option, UnityEvent> onChangedOption = new Dictionary<Option, UnityEvent>();

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
        options[(int)Option.MaxHeart] = 1.0f;
        options[(int)Option.CurHeart] = options[(int)Option.MaxHeart];
		options[(int)Option.Speed] = 3.0f; 
	}
     
    public float GetStat(Option option) => options[(int)option];
    public void AddStat(Option option, float value)
    {
		options[(int)option] = Math.Max(0f, options[(int)option] + value);

        if (onChangedOption.TryGetValue(option, out UnityEvent e))
            e.Invoke();


	}

	void UpdateHeartSystem(Option heart, float value)
    {
        float total = GetStat(Option.MaxHeart) + GetStat(Option.BlackHeart) + GetStat(Option.SoulHeart);
        if (total < 12)
			options[(int)heart] += value;
		
        else if (heart == Option.BlackHeart)
        {
            if (GetStat(Option.SoulHeart) > 0.0f)
            {
                AddStat(Option.SoulHeart, -value);
                AddStat(heart, value);
            }    
            else
            {
				AddStat(Option.MaxHeart, -value);
				AddStat(heart, value);
			}
        }
        else
        {
            Debug.Log("먹을 수 없습니다."); 
        }
    }
   
    public void AddListener(Option option, Action lilstener)
    {
        if (onChangedOption.ContainsKey(option) == false)
            onChangedOption.Add(option, new UnityEvent());

        onChangedOption[option].AddListener(() => lilstener.Invoke());
	}


} 