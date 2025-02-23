// 예시: 플레이어가 게임에 입장했을 때 발생하는 이벤트
public class PlayerDamagedEvent : Event
{
    public string PlayerName { get; }

    public float Damage { get; }

    public PlayerDamagedEvent(string playerName, float damage)
    {
        PlayerName = playerName;
        Damage = damage;
    }
}