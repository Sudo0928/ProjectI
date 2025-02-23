using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DesignEnums;

[Serializable]
public class PlayerData
{
	public Dictionary<Option, float> options = new Dictionary<Option, float>()
	{
		{Option.Attack, 1.0f },
		{Option.AttackSpeed, 1.0f },
		{Option.AttackScale, 1.0f },
		{Option.Range, 1.0f },
		{Option.RangeScale, 1.0f },
		{Option.ProjectileSize, 1.0f },
		{Option.ProjectileSpeed, 1.0f },
		{Option.Speed, 1.0f },
		{Option.Health, 1.0f },
	};

	public void AddItem(ItemInfo item)
	{
		int size = item.OptionValues.Count;
		for (int i = 0; i < size; i++)
		{
			int idx = item.AvailableOptions[i];
			float value = item.OptionValues[i];
			var target = DataManager.itemOptionLoader.GetByKey(idx);

			options[target.Name] += value;
		}
	}

	public void RemoveItem(ItemInfo item)
	{
		int size = item.OptionValues.Count;
		for (int i = 0; i < size; i++)
		{
			int idx = item.AvailableOptions[i];
			float value = item.OptionValues[i];
			var target = DataManager.itemOptionLoader.GetByKey(idx);

			options[target.Name] -= value; 
		}
	}
}