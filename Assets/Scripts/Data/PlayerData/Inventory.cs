using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DesignEnums;

public class Inventory
{
	HashSet<SpecialAbility> specialAbilitys = new HashSet<SpecialAbility>();
	Dictionary<Option, int> myItems = new Dictionary<Option, int>();
	public UnityEvent onAddItem = new UnityEvent();
	 
	public Stat stat { private get; set; }

	public void AddItem(ItemInfo item)
	{
		int size = item.OptionValues.Count;
		for (int i = 0; i < size; i++)
		{
			int idx = item.AvailableOptions[i];
			float value = item.OptionValues[i];
			var target = DataManager.itemOptionLoader.GetByKey(idx);

			if (target != null)
			{
				stat.AddStat(target.Name, value);
				if (myItems.ContainsKey(target.Name))
					myItems[target.Name] += 1;
				else
					myItems.Add(target.Name, 1);
			} 
		}

		foreach (int idx in item.SpecialOptions)
		{
			var sp = DataManager.specialAbilityInfoLoader.GetByKey(idx);
			if (sp != null)
			{
				var skill = DataManager.specialAbilityData.GetSpecialAbility(sp.ComponentName);
				if (skill != null)
				{
					//skill.OnAbility(playerController); 
					specialAbilitys.Add(skill);
				}
			}
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

			myItems[target.Name] -= 1;
			if (myItems[target.Name] == 0)
				myItems.Remove(target.Name); 
		}
	}



}