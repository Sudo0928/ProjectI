using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBrimstone : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.RegisterListener<PlayerAttackEvent>(Attack, 1);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<PlayerAttackEvent>(Attack);
    }

    private void Attack(PlayerAttackEvent e)
    {
        PlayerController player = e.player;
        //Vector2 direction = player.LookDirection
    }
}
