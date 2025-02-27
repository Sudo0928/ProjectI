using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
	[NonSerialized] public UnityEvent onPickupItem = new UnityEvent();
    [SerializeField] SpriteRenderer _sprite; 
    [SerializeField] int key;
	ItemInfo item;
	BoxCollider2D collider;

	private bool isPickUp = false;

	private void Awake()
	{
		collider = GetComponent<BoxCollider2D>();	
		item = DataManager.itemInfoLoader.GetByKey(key);
		if (item != null)
			_sprite.sprite = GameManager.Instance.GetItemSprite(item);
	}
	 
	public void SetActiveCollider(bool active) => collider.enabled = active;

	public void SetItem(ItemInfo item)
	{ 
		this.item = item;
		key = item.key;
		_sprite.sprite = GameManager.Instance.GetItemSprite(item);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			
			if (collision.gameObject.TryGetComponent<Player>(out Player player))
			{
				player.AddItem(item);
				transform.SetParent(player.pickUpPivot);
				transform.localPosition = Vector3.zero;
				_sprite.sortingOrder = 120;
				transform.localScale = Vector3.one * 0.8f;
				isPickUp = true;
            }
			var pu = collision.gameObject.GetComponent<PlayerUIHandler>();
			pu?.myPickupItemInfoUI.PickupItem(item);

            Action action = () => gameObject.SetActive(false);
            GameManager.Instance.SetTimer(action, 1f);

			onPickupItem?.Invoke();
		} 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			var pu = collision.gameObject.GetComponent<PlayerUIHandler>();
			pu?.myItemPreviewUI.EnterItem(gameObject, item);
		}
	}
	private void OnTriggerExit2D(Collider2D collision) 
	{
		if (collision.CompareTag("Player"))
		{
			var pu = collision.gameObject.GetComponent<PlayerUIHandler>();
			pu?.myItemPreviewUI.ExitItem(gameObject);
		}  
	}

	float time = 0.0f;
	int d = 1;
	private void Update()
	{
		if (isPickUp) return;

		var pos = transform.position;
		pos.y += Time.deltaTime * d * 0.3f;
		transform.position = pos;
		time += Time.deltaTime * d * 0.3f;

		if (time >= 0.3f || time <= 0.0f)
			d *= -1;
		
	}
}
