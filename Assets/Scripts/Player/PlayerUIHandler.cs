using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
	[SerializeField] GameObject itemInfoUIPrefab;
	[SerializeField] GameObject itemPreviewUIPrefab;
	PickupItemInfoUI itemInfoUI;
	ItemPreviewUI itemPreviewUI;

	public PickupItemInfoUI myPickupItemInfoUI => itemInfoUI;
	public ItemPreviewUI myItemPreviewUI => itemPreviewUI;

	private void Awake()
	{
		itemInfoUI = Instantiate<GameObject>(itemInfoUIPrefab).GetComponent<PickupItemInfoUI>();
		itemPreviewUI = Instantiate<GameObject>(itemPreviewUIPrefab).GetComponent<ItemPreviewUI>();
		itemPreviewUI.player = gameObject;
	}
}
