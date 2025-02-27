using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryBaby : SpecialAbility
{
	[SerializeField] GameObject dryBaby;
	public override void OnAbility(PlayerController player)
	{
		var go = Instantiate<GameObject>(dryBaby);
		go.GetComponent<Familiar_DryBaby>().player = player;
		go.transform.position = player.transform.position + Vector3.up * 0.5f;
	} 

	public override void RemoveSkill()
	{
		
	}



}
