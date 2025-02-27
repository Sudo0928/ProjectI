// ����: �÷��̾ ���ӿ� �������� �� �߻��ϴ� �̺�Ʈ
using UnityEngine;

public class PlayerAttackEvent : Event
{
    public Player player;

    public Vector2 direction = Vector2.zero;

    public PlayerAttackEvent(Player player, Vector2 direction)
    {
        this.player = player;
        this.direction = direction;
    }
}