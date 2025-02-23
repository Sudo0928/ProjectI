using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DesignEnums;

[Serializable]
public class PlayerData 
{
	float[] options;
	HashSet<SpecialAbility> specialAbilitys = new HashSet<SpecialAbility>();

	// 아이템 아이디, 아이템 개수
	Dictionary<int, int> myItems = new Dictionary<int, int>();

	public PlayerData()
	{
		options = new float[Enum.GetValues(typeof(Option)).Length];
		options[(int)Option.Attack] = 1.0f;
		options[(int)Option.AttackSpeed] = 1.0f;
		options[(int)Option.AttackScale] = 1.0f;
		options[(int)Option.Range] = 1.0f;
		options[(int)Option.RangeScale] = 1.0f;
		options[(int)Option.ProjectileSize] = 1.0f;
		options[(int)Option.ProjectileSpeed] = 1.0f;
		options[(int)Option.Health] = 1.0f;
	}

	public float GetOptionValue(Option option)
	{
		return options[(int)option];
	} 

	public void AddItem(ItemInfo item)
	{
		int size = item.OptionValues.Count;
		for (int i = 0; i < size; i++)
		{
			int idx = item.AvailableOptions[i];
			float value = item.OptionValues[i];
			var target = DataManager.itemOptionLoader.GetByKey(idx);

			options[(int)target.Name] += 1.0f;
			if (myItems.ContainsKey((int)target.Name))
				myItems[(int)target.Name] += 1;
			else
				myItems.Add((int)target.Name, 1);
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

			int ot = (int)target.Name;
			options[ot] -= value;

			myItems[ot] -= 1;
			if (myItems[ot] == 0)
				myItems.Remove(ot);
		}
	}
}