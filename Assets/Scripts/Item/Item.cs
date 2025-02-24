using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] int key;

	private void Awake()
	{
		_sprite = GetComponent<SpriteRenderer>();

		var item = DataManager.itemInfoLoader.GetByKey(key);
		if (item != null)
			_sprite.sprite = Resources.Load<Sprite>("images/items/"+item.Image);

		gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
	}
	 
	public void SetItem(ItemInfo item)
	{
		_sprite.sprite = Resources.Load<Sprite>("images/items/" + item.Image);
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerControl player = collision.GetComponent<PlayerControl>();

            CricketsHead head = new CricketsHead(player);
            player.effects.Add(head);

			Destroy(gameObject);
        }
    }
}
