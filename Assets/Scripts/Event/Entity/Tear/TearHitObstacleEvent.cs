using UnityEngine;

public class TearHitObstacleEvent : Event
{
    public BaseTear tear;
    public Collision2D hitCollision;
    public Vector2 hitDirection;

    public TearHitObstacleEvent(BaseTear tear, Collision2D hitCollision, Vector2 hitDirection)
    {
        this.tear = tear;
        this.hitCollision = hitCollision;
        this.hitDirection = hitDirection;
    }
}