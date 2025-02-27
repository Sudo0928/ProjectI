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

    private void Explosion(TearDestroyEvent e)
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(e.tear.transform.position, 1);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].TryGetComponent<IDamagedable>(out var entity))
            {
                entity.TakeBoomDamage(10);
            }
        }
    }

	public override void OnAbility(Player player)
	{
		EventManager.RegisterListener<TearDestroyEvent>(Explosion, 2);
		EventManager.RegisterListener<TearLaunchEvent>(ChangeColor, 1);
	}

	public override void RemoveSkill()
	{
		EventManager.UnregisterListener<TearDestroyEvent>(Explosion);
		EventManager.UnregisterListener<TearLaunchEvent>(ChangeColor);
	}
}
