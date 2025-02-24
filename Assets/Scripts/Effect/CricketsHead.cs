using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : Effect
{
    public CricketsHead(PlayerControl player)
    {
        player.CurrentHP = player.MaxHP;
    }

    ~CricketsHead()
    {
        EventManager.UnregisterListener<AddItemEvent>(Test);
    }

    public void Init()
    {
        EventManager.RegisterListener<AddItemEvent>(Test);
    }

    private void Test(AddItemEvent e)
    {

    }
}
