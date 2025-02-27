using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
	[SerializeField] GameObject itemInfoUIPrefab;
	[SerializeField] GameObject itemPreviewUIPrefab;
	[SerializeField] GameObject playerStatUIPrefab;
	PickupItemInfoUI itemInfoUI;
	ItemPreviewUI itemPreviewUI;
	PlayerStatUI playerStatUI;

	public PickupItemInfoUI myPickupItemInfoUI => itemInfoUI;
	public ItemPreviewUI myItemPreviewUI => itemPreviewUI;
	public PlayerStatUI StatUI => playerStatUI; 

	private void Awake()
	{
		itemInfoUI = Instantiate<GameObject>(itemInfoUIPrefab).GetComponent<PickupItemInfoUI>();
		itemPreviewUI = Instantiate<GameObject>(itemPreviewUIPrefab).GetComponent<ItemPreviewUI>();
		playerStatUI = Instantiate<GameObject>(playerStatUIPrefab).GetComponent<PlayerStatUI>();
		playerStatUI.playerStat = gameObject.GetComponent<PlayerController>()?.Stat;
		itemPreviewUI.player = gameObject;
	}
}
