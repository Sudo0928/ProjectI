using UnityEngine;

public class GameEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		// 리스너 등록
		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerJoin, 10);
		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerJoinLow, 5);
	}

	private void OnDisable()
	{
		// 리스너 해제
		EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerJoin);
		EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerJoinLow);
	}

	// 플레이어가 게임에 입장했을 때 처리하는 이벤트 핸들러 (우선순위 높은 리스너)
	private void OnPlayerJoin(PlayerDamagedEvent e)
	{
		Debug.Log($"[High Priority] Player Damaged: {e.PlayerName}");
	}

	// 또 다른 플레이어 입장 이벤트 핸들러 (우선순위 낮음)
	private void OnPlayerJoinLow(PlayerDamagedEvent e)
	{
		Debug.Log($"[Low Priority] Player : {e.PlayerName} / Damage : {e.Damage}");
	}
}