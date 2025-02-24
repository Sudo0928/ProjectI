using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityData
{
	Dictionary<string, SpecialAbility> specialAbilitys = new Dictionary<string, SpecialAbility>();

	public SpecialAbilityData() 
	{
		List<SpecialAbilityInfo> list = DataManager.specialAbilityInfoLoader.ItemsList;

		foreach (var info in list)
		{
			Type type = Type.GetType(info.ComponentName) ;
			if (type != null)
			{
				var t = Activator.CreateInstance(type);
				GameObject abilityObject = new GameObject(info.ComponentName);
				SpecialAbility ability = abilityObject.AddComponent(type) as SpecialAbility;
				specialAbilitys.Add(info.ComponentName, ability);
			}
		}
	}

	public SpecialAbility GetSpecialAbility(string componentName)
	{
		if (specialAbilitys.ContainsKey(componentName))
			return specialAbilitys[componentName];
		return null;
	}
}
