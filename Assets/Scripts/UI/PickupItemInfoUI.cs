using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PickupItemInfoUI : MonoBehaviour
{ 
    [SerializeField] Text _name;
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

		_name.text = item.Name;   
        description.text = item.Massage;
        gameObject.SetActive(true);
        GameManager.Instance.SetTimer(() => { anim.Play("Close");}, 3.0f); 
	} 
} 
