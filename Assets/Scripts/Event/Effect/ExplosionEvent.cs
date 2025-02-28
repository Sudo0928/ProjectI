using UnityEngine;

public class ExplosionEvent : Event
{
    public Vector2 position;

    public ExplosionEvent(Vector2 position)
    {
        this.position = position;
    }
}
