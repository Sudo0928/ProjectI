using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private void Start()
	{

	}

	[SerializeField] public BaseTear tear;
	[SerializeField] public BaseTear enemyTear;

	public Sprite GetItemSprite(ItemInfo item)
	{
		return Resources.Load<Sprite>("images/items/" + item.Image);
	}

	public T Instantiate<T>(T prefab) where T : UnityEngine.Object
    {
		var go = UnityEngine.Object.Instantiate<T>(prefab);
		return go;
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

	public void SetTimer(Action action, float time)
	{
		StartCoroutine(Timer(action, time));

	}

	IEnumerator Timer(Action action, float time)
	{
		yield return new WaitForSeconds(time);
		action?.Invoke();
	}

}
