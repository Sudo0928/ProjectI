using UnityEngine;

public class GameEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		// ������ ���
		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerJoin, 10);
		EventManager.RegisterListener<PlayerDamagedEvent>(OnPlayerJoinLow, 5);
	}

	private void OnDisable()
	{
		// ������ ����
		EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerJoin);
		EventManager.UnregisterListener<PlayerDamagedEvent>(OnPlayerJoinLow);
	}

	// �÷��̾ ���ӿ� �������� �� ó���ϴ� �̺�Ʈ �ڵ鷯 (�켱���� ���� ������)
	private void OnPlayerJoin(PlayerDamagedEvent e)
	{
		Debug.Log($"[High Priority] Player Damaged: {e.PlayerName}");
	}

	// �� �ٸ� �÷��̾� ���� �̺�Ʈ �ڵ鷯 (�켱���� ����)
	private void OnPlayerJoinLow(PlayerDamagedEvent e)
	{
		Debug.Log($"[Low Priority] Player : {e.PlayerName} / Damage : {e.Damage}");
	}
}