using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;

public class MagicMushroom : SpecialAbility
{
	public override void OnAbility(Player player)
	{
		player.Stat.CurHp = player.Stat.GetStat(Option.Heart); 
	} 

	public override void RemoveSkill()
	{
		throw new System.NotImplementedException();
	}

}
