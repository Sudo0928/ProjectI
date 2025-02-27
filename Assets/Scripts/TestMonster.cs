using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestMonster : MonoBehaviour
{
	bool isDie = false;
	public UnityEvent onDie = new UnityEvent();

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isDie && collision.CompareTag("Tear"))
		{ 
			onDie?.Invoke();
			isDie = true;
			gameObject.SetActive(false);
		}
	}
}
 
