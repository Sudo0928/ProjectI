using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DesignEnums;

public class Inventory
{
	HashSet<SpecialAbility> specialAbilitys = new HashSet<SpecialAbility>();
	Dictionary<ItemInfo, int> myItems = new Dictionary<ItemInfo, int>();
	public UnityEvent onAddItem = new UnityEvent();
	ItemInfo myActiveItem = null;

	Stat stat = null;
	PlayerController player = null;

	public void InitInventory(Stat stat, PlayerController player)
	{
		this.stat = stat;
		this.player = player;
	}

	public void AddItem(ItemInfo item)
	{
		int size = item.OptionValues.Count;
		if (!myItems.ContainsKey(item))
			myItems.Add(item, 0);

		myItems[item] += 1;

		for (int i = 0; i < size; i++)
		{
			int idx = item.AvailableOptions[i];
			float value = item.OptionValues[i];
			var target = DataManager.itemOptionLoader.GetByKey(idx);

			if (target != null)
				stat.AddStat(target.Name, value);
		}

		foreach (int idx in item.SpecialOptions)
		{
			var sp = DataManager.specialAbilityInfoLoader.GetByKey(idx);
			if (sp != null)
			{
				var skill = DataManager.specialAbilityData.GetSpecialAbility(sp.ComponentName);
				if (skill != null)
				{
					skill.OnAbility(player);  
					specialAbilitys.Add(skill);
				}
			}
		}

		// 액티브 아이템 이라면
		if (item.Gauge > 0)
		{
			if (myActiveItem != null)
				myItems.Remove(myActiveItem);

			myActiveItem = item;
			player.PlayerUIHandler.StatUI.UpdateActiveItem(myActiveItem);
			stat.MaxGauge = item.Gauge;
			stat.CurGauge = item.Gauge;
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

			stat.AddStat(target.Name, -value);

			myItems[item] -= 1; 
			if (myItems[item] == 0)
				myItems.Remove(item); 
		}
	}



}