using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HaloOfFlies : SpecialAbility
{
    [SerializeField] GameObject fliesPrefab;
	GameObject files;

	public override void OnAbility(Player player)
	{
		files = GameManager.Instance.Instantiate<GameObject>(fliesPrefab);
		files.transform.SetParent(player.gameObject.transform, false);
		files.transform.position = player.gameObject.transform.position;

		 
	}

	public override void RemoveSkill()
	{
		 
	}
}
