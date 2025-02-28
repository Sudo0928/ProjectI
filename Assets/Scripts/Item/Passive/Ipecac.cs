using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : SpecialAbility
{
    private void OnEnable()
    {
        //EventManager.RegisterListener<TearDestroyEvent>(Explosion, 2);
        //EventManager.RegisterListener<TearLaunchEvent>(ChangeColor, 1);
    }

    private void OnDisable()
    {
        //EventManager.UnregisterListener<TearDestroyEvent>(Explosion);
        //EventManager.UnregisterListener<TearLaunchEvent>(ChangeColor);
    }

    private void ChangeColor(TearLaunchEvent e)
    {
        e.tear.TearSprite.color = new Color(0, 0.6f, 0);
    }

    private void Explosion(TearDropEvent e)
    {
        ExplosionEvent explosionEvent = new ExplosionEvent(e.tear.transform.position);
        EventManager.DispatchEvent(explosionEvent);

        Collider2D[] collider2D = Physics2D.OverlapCircleAll(e.tear.transform.position, 1);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].TryGetComponent<IDamagedable>(out var entity))
            {
                entity.TakeBoomDamage(10);
            }
        }

        e.Cancel();
    }

    private void Explosion(TearHitEntityEvent e)
    {
        ExplosionEvent explosionEvent = new ExplosionEvent(e.tear.transform.position);
        EventManager.DispatchEvent(explosionEvent);

        Collider2D[] collider2D = Physics2D.OverlapCircleAll(e.tear.transform.position, 1);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].TryGetComponent<IDamagedable>(out var entity))
            {
                entity.TakeBoomDamage(10);
            }
        }

        e.Cancel();
    }

    private void Explosion(TearHitObstacleEvent e)
    {
        ExplosionEvent explosionEvent = new ExplosionEvent(e.tear.transform.position);
        EventManager.DispatchEvent(explosionEvent);

        Collider2D[] collider2D = Physics2D.OverlapCircleAll(e.tear.transform.position, 1);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].TryGetComponent<IDamagedable>(out var entity))
            {
                entity.TakeBoomDamage(10);
            }
        }

        e.Cancel();
    }

    public override void OnAbility(PlayerController player)
	{
		EventManager.RegisterListener<TearDropEvent>(Explosion, 2);
		EventManager.RegisterListener<TearHitObstacleEvent>(Explosion, 2);
		EventManager.RegisterListener<TearHitEntityEvent>(Explosion, 2);
		EventManager.RegisterListener<TearLaunchEvent>(ChangeColor, 1);
	}

	public override void RemoveSkill()
	{
		EventManager.UnregisterListener<TearDropEvent>(Explosion);
		EventManager.UnregisterListener<TearHitObstacleEvent>(Explosion);
		EventManager.UnregisterListener<TearHitEntityEvent>(Explosion);
		EventManager.UnregisterListener<TearLaunchEvent>(ChangeColor);
	}
}
