using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] int key;
	ItemInfo item;
	private void Awake()
	{
		item = DataManager.itemInfoLoader.GetByKey(key);
		if (item != null)
			_sprite.sprite = Resources.Load<Sprite>("images/items/"+item.Image);
	}
	 
	public void SetItem(ItemInfo item)
	{
		this.item = item;
		key = item.key;
		_sprite.sprite = Resources.Load<Sprite>("images/items/" + item.Image);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<PlayerController>().playerData.AddItem(item);
			gameObject.SetActive(false);
		} 
	}

}
