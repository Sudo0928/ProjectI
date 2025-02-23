using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] int key;

	private void Awake()
	{
		var item = DataManager.itemInfoLoader.GetByKey(key);
		if (item != null)
			_sprite.sprite = Resources.Load<Sprite>("images/items/"+item.Image);
	}
	 
	public void SetItem(ItemInfo item)
	{
		_sprite.sprite = Resources.Load<Sprite>("images/items/" + item.Image);
	}
}
