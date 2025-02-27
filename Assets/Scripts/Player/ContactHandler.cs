using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactHandler : MonoBehaviour
{
    GameObject parent;

	private void Awake()
	{
		parent = transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		
	}

}
