using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;

public class MagicMushroom : SpecialAbility
{
	public override void OnAbility(PlayerController pc)
	{
		pc.playerData.CurHp = pc.playerData.GetOptionValue(Option.Heart);
		Debug.Log("체력 회복");

	} 

	public override void RemoveSkill()
	{
		throw new System.NotImplementedException();
	}

}
