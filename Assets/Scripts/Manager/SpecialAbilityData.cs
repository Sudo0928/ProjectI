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
				specialAbilitys.Add(info.ComponentName, Activator.CreateInstance(type) as SpecialAbility);
		}
	}

	public SpecialAbility GetSpecialAbility(string componentName)
	{
		if (specialAbilitys.ContainsKey(componentName))
			return specialAbilitys[componentName];
		return null;
	}
}
