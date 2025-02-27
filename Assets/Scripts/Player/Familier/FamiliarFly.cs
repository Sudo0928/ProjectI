using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarFly : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Tear"))
		{
			var Tear = collision.GetComponent<BaseTear>();
			if (Tear.Owner.gameObject.CompareTag("Monster"))
			{
		//		Tear.
			}


		}
	}
}
