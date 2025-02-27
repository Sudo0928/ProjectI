using UnityEngine;
using static DesignEnums;

public class MutantSpider : SpecialAbility
{
    public override void OnAbility(PlayerController player)
    {
        EventManager.RegisterListener<PlayerAttackEvent>(Attack, 1);
    }

    public override void RemoveSkill()
    {
        EventManager.UnregisterListener<PlayerAttackEvent>(Attack);
    }

    public void Attack(PlayerAttackEvent e)
    {
        for(int i = 0; i < 3; i++)
        {
            float randomX = 0;
            float randomY = 0;

            randomX = randomX.GetRange(-1f, 1f);
            randomY = randomX.GetRange(-1f, 1f);

            randomX = e.player.LookDirection.x != 0 ? e.player.LookDirection.x : randomX;
            randomY = e.player.LookDirection.y != 0 ? e.player.LookDirection.y : randomY;

            Vector2 direction = new Vector2(randomX, randomY).normalized;

            BaseTear tear = Instantiate(GameManager.Instance.tear, e.player.transform.position, Quaternion.identity);
            tear.Init(e.player.gameObject, e.player.projectileSpeed, e.player.Stat.GetStat(Option.Attack), e.player.projectileDistance, e.player.projectileSize, direction, e.player.isParbolic);
        }
    }
}
