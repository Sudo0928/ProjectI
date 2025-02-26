using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		// ������ ���
		EventManager.RegisterListener<PlayerJoinEvent>(OnPlayerJoin, 5);
		EventManager.RegisterListener<PlayerJoinEvent>(OnPlayerJoinLow, 10);

		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerDamaged, 10);
		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerDamagedLow, 5);

		EventManager.RegisterListener<AddItemEvent>(OnAddItemEvent, 10);
	}

	private void OnDisable()
	{
        // ������ ����
        EventManager.UnregisterListener<PlayerJoinEvent>(OnPlayerJoin);
        EventManager.UnregisterListener<PlayerJoinEvent>(OnPlayerJoinLow);

        EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerDamaged);
		EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerDamagedLow);
	}

	// �÷��̾ ���ӿ� �������� �� ó���ϴ� �̺�Ʈ �ڵ鷯 (�켱���� ���� ������)
	private void OnPlayerDamaged(PlayerDamagedEvent e)
	{
		Debug.Log($"[High Priority] Player Damaged: {e.PlayerName}");
	}

	// �� �ٸ� �÷��̾� ���� �̺�Ʈ �ڵ鷯 (�켱���� ����)
	private void OnPlayerDamagedLow(PlayerDamagedEvent e)
	{
		Debug.Log($"[Low Priority] Player : {e.PlayerName} / Damage : {e.Damage}");
	}
	
	private void OnPlayerJoin(PlayerJoinEvent e)
	{
		Debug.Log($"Welcome to my game : {e.PlayerName}");
	}

    private void OnPlayerJoinLow(PlayerJoinEvent e)
    {
        Debug.Log($"This game is xxxxxxx : {e.PlayerName}");
    }

	private void OnAddItemEvent(AddItemEvent e)
	{
		Debug.Log("Test"); 
		Debug.Log($"Add Item : {e.itemInfo.Name}");
		e.pickupItemInfoUI.PickupItem(e.itemInfo);
	}



	// ��¡�� �Ұ���, ����ü�� � �������
	// ��¡ Event (����ü prefab, bool ��¡ �Ӱ���)
	
	// 
}