using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private void Start()
	{

	}

	public new void StartCoroutine(IEnumerator enumerator)
	{
		base.StartCoroutine(enumerator);
	}

	public new void StopCoroutine(IEnumerator enumerator)
	{
        base.StopCoroutine(enumerator);
    }

    public new void StopAllCoroutines()
    {
        base.StopAllCoroutines();
    }
}
