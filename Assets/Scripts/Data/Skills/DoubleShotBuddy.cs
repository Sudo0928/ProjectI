using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShotBuddy : SpecialAbility
{
    [SerializeField] GameObject harlequin;

	public override void OnAbility(Player player)
	{
		var go = Instantiate<GameObject>(harlequin);
		var har = go.GetComponent<HarlequinBuddy>();
		har.player = player;
		EventManager.RegisterListener<PlayerAttackEvent>(har.Attack, 0);

	}

	public override void RemoveSkill()
	{
		
	}
}
