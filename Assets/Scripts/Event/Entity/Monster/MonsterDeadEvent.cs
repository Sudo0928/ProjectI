public class MonsterDeadEvent : Event
{
    public MonsterBasic enemy;

    public MonsterDeadEvent(MonsterBasic enemy)
    {
        this.enemy = enemy;
    }
}
