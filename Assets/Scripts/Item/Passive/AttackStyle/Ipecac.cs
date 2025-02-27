using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.RegisterListener<TearDestroyEvent>(Explosion, 1);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<TearDestroyEvent>(Explosion);
    }

    private void Explosion(TearDestroyEvent e)
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(e.tear.transform.position, 1);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].TryGetComponent<IDamagedable>(out var entity))
            {
                entity.TakeDamage(e.tear);
            }
        }
    }
}
