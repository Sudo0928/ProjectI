using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
	public UnityEvent onPickupItem;
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
			onPickupItem?.Invoke();
		} 
	}

	float time = 0.0f;
	int d = 1;
	private void Update()
	{
		var pos = transform.position;
		pos.y += Time.deltaTime * d * 0.3f;
		transform.position = pos;
		time += Time.deltaTime * d * 0.3f;

		if (time >= 0.3f || time <= 0.0f)
			d *= -1;
		
	}
}
