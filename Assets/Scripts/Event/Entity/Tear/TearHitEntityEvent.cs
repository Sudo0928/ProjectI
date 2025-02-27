using UnityEngine;

public class TearHitEntityEvent : Event
{
    public Collision2D entity;
    public BaseTear tear;

    public TearHitEntityEvent(BaseTear tear, Collision2D entity)
    {
        this.tear = tear;
        this.entity = entity;
    }
}