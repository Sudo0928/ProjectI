using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbility : Component
{
	// TODO 함수 추가
    public abstract void AddSkill();
    public abstract void RemoveSkill();
}

public class TESTSKILL : SpecialAbility
{
	public override void AddSkill()
	{
		throw new System.NotImplementedException();
	}

	public override void RemoveSkill()
	{
		throw new System.NotImplementedException();
	}
}