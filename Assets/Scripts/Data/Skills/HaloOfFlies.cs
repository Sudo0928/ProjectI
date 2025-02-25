using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HaloOfFlies : SpecialAbility
{
    [SerializeField] GameObject fliesPrefab;
	GameObject files;

	public override void OnAbility(PlayerController pc)
	{
		files = GameManager.Instance.Instantiate<GameObject>(fliesPrefab);
		files.transform.SetParent(pc.gameObject.transform, false);
		files.transform.position = pc.gameObject.transform.position;


	}

	public override void RemoveSkill()
	{
		 
	}
}
