using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityData
{
	Dictionary<string, Type> specialAbilitys = new Dictionary<string, Type>();


	public SpecialAbilityData() 
	{
		List<SpecialAbilityInfo> list = DataManager.specialAbilityInfoLoader.ItemsList;

		foreach (var info in list)
		{
			Type type = Type.GetType(info.ComponentName) ;
			if (type != null)
				specialAbilitys.Add(info.ComponentName, type);
		}


	}

}
