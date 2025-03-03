using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;

public class ChocoMilk : SpecialAbility
{
    private PlayerController player;

    public override void OnAbility(PlayerController player)
    {
        this.player = player;

        player.isCharging = true;
        player.Stat.AddStat(Option.ProjectileSize, 0.25f);
    }

    public override void RemoveSkill()
    {
        player.isCharging = false;
    }
}
