// ����: �÷��̾ ���ӿ� �������� �� �߻��ϴ� �̺�Ʈ
using UnityEngine;

public class PlayerAttackEvent : Event
{
    public PlayerController player;

    public Vector2 direction = Vector2.zero;

    public PlayerAttackEvent(PlayerController player, Vector2 direction)
    {
        this.player = player;
        this.direction = direction;
    }
}