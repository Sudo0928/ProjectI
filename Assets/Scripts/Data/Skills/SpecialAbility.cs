using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbility : MonoBehaviour
{
	// TODO �Լ� �߰�
    public abstract void OnAbility(PlayerController pc);
    public abstract void RemoveSkill();
}

public class TESTSKILL : SpecialAbility
{
	public override void OnAbility(PlayerController pc)
	{
		throw new System.NotImplementedException();
	}

	public override void RemoveSkill()
	{
		throw new System.NotImplementedException();
	}
}