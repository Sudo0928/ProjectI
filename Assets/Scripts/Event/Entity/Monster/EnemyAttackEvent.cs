using UnityEngine;

public class EnemyAttackEvent  : Event
{
    public float damage;
    public Collision2D entity;

    public EnemyAttackEvent(float damage, Collision2D entity)
    {
        this.damage = damage;
        this.entity = entity;
    }
}