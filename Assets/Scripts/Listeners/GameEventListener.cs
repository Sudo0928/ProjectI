using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		// 리스너 등록
		EventManager.RegisterListener<PlayerJoinEvent>(OnPlayerJoin, 5);
		EventManager.RegisterListener<PlayerJoinEvent>(OnPlayerJoinLow, 10);

		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerDamaged, 10);
		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerDamagedLow, 5);

		EventManager.RegisterListener<AddItemEvent>(OnAddItemEvent, 10);
	}

	private void OnDisable()
	{
        // 리스너 해제
        EventManager.UnregisterListener<PlayerJoinEvent>(OnPlayerJoin);
        EventManager.UnregisterListener<PlayerJoinEvent>(OnPlayerJoinLow);

        EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerDamaged);
		EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerDamagedLow);
	}

	// 플레이어가 게임에 입장했을 때 처리하는 이벤트 핸들러 (우선순위 높은 리스너)
	private void OnPlayerDamaged(PlayerDamagedEvent e)
	{
		Debug.Log($"[High Priority] Player Damaged: {e.PlayerName}");
	}

	// 또 다른 플레이어 입장 이벤트 핸들러 (우선순위 낮음)
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



	// 차징을 할건지, 투사체를 몇개 만들건지
	// 차징 Event (투사체 prefab, bool 차징 켤건지)
	
	// 
}