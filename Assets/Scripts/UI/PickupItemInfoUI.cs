using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PickupItemInfoUI : MonoBehaviour
{
    [SerializeField] Text name;
    [SerializeField] Text description;
    Animator anim;
	private void Start()
	{
        gameObject.SetActive(false);
		anim = GetComponent<Animator>();    
	}

	public void PickupItem(ItemInfo item) 
    {
        gameObject.SetActive(true);
        anim.Play("Open");

		name.text = item.Name;   
        description.text = item.Description;
        gameObject.SetActive(true);
        GameManager.Instance.SetTimer(() => { anim.Play("Close");}, 3.0f); 
	} 
} 
