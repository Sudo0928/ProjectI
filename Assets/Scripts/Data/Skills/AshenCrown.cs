using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshenCrown : Skill, IPassiveSkill
{
	Coroutine mySkill;

	public void OffPassive()
	{
		if (mySkill != null)
			StopCoroutine(mySkill);
	}

	public void OnPassive()
	{
		mySkill = StartCoroutine(PlayAshenCrown());
	}


	IEnumerator PlayAshenCrown()
	{
		while (true)
		{
			Debug.Log("체력 1 회복!");
			yield return new WaitForSeconds(3f);
		}
	}
}
