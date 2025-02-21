using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavensTear : Skill, IActiveSkill
{
	[SerializeField] GameObject tearPrefab;
	public void Action()
	{ 
		var go = Instantiate<GameObject>(tearPrefab);
		go.transform.position = transform.position;
		var rigid = go.GetComponent<Rigidbody2D>();
		rigid.velocity = new Vector2(0, 100);
	}
}
