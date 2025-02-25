using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DesignEnums;

[Serializable]
public class PlayerData 
{
	HashSet<SpecialAbility> specialAbilitys = new HashSet<SpecialAbility>();
	Dictionary<int, int> myItems = new Dictionary<int, int>();
	PlayerController playerController;
	float[] options;

	// 아이템 아이디, 아이템 개수
	float curHp = 0f;
	public float CurHp { get { return curHp; } set { curHp = value; } } 

	public PlayerData(PlayerController pc)
	{
		playerController = pc;
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
		CurHp = options[(int)Option.Heart];
	}

	public float GetOptionValue(Option option)
	{
		return options[(int)option];
	} 

	public void AddItem(ItemInfo item)
	{
		Debug.Log(item.Massage);
		int size = item.OptionValues.Count;
		for (int i = 0; i < size; i++)
		{
			int idx = item.AvailableOptions[i];
			float value = item.OptionValues[i];
			var target = DataManager.itemOptionLoader.GetByKey(idx);

			if (target != null)
			{
				options[(int)target.Name] += value;
				if (myItems.ContainsKey((int)target.Name))
					myItems[(int)target.Name] += 1;
				else
					myItems.Add((int)target.Name, 1);
			}
			 
		}

		foreach (int idx in item.SpecialOptions)
		{
			var sp = DataManager.specialAbilityInfoLoader.GetByKey(idx);
			if (sp != null)
			{
				var skill =DataManager.specialAbilityData.GetSpecialAbility(sp.ComponentName);
				if (skill != null)
				{
					skill.OnAbility(playerController);
					specialAbilitys.Add(skill);
				}
			}
			
		}

		AddItemEvent addItem = new AddItemEvent(this, item);
		EventManager.DispatchEvent(addItem);
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