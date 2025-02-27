// 예시: 플레이어가 게임에 입장했을 때 발생하는 이벤트
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