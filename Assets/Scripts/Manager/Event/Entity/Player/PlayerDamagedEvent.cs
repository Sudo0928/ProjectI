// ����: �÷��̾ ���ӿ� �������� �� �߻��ϴ� �̺�Ʈ
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