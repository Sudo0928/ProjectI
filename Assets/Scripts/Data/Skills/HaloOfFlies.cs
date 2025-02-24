using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloOfFlies : SpecialAbility
{
    [SerializeField] GameObject fliesPrefab;

	GameObject files;
	public override void OnAbility(PlayerController pc)
	{ 
		files = Instantiate<GameObject>(fliesPrefab);
		files.transform.SetParent(pc.gameObject.transform, false);	
		files.transform.position = pc.gameObject.transform.position;
	} 
	 
	public override void RemoveSkill()
	{
		Destroy(files); 
	}
}
