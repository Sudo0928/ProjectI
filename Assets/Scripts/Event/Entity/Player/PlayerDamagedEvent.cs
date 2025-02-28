public class PlayerDamagedEvent : Event
{
    public float damage;

    public PlayerDamagedEvent(float damage)
    {
        this.damage = damage;
    }
}


public class PlayerDieEvent : Event
{ 

    public PlayerDieEvent()
    {

    }
}

