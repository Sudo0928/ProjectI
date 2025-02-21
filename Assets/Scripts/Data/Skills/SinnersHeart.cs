using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinnersHeart : Skill, IActiveSkill
{
	public void Action()
	{
		Debug.Log("체력을 1 깎고, 공격력 상승");
	}
}
