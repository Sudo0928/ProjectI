using UnityEngine;

public class BurstingShots : SpecialAbility
{
    private BaseTear tear;

    private void OnEnable()
    {
        //EventManager.RegisterListener<TearDestroyEvent>(Division, 1);
    }

    private void OnDisable()
    {
        //EventManager.UnregisterListener<TearDestroyEvent>(Division);
    }
     
    private void Division(TearDropEvent e)
    {
        this.tear = e.tear;

        Division();
    }

    private void Division(TearHitObstacleEvent e)
    {
        this.tear = e.tear;

        Division();
    }

    private void Division()
    {
        if (this.tear.Size < 0.5f) return;

        BaseTear temptear = tear;

        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        temptear = Instantiate(temptear);
        temptear.Init(tear.Owner, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(randomX, randomY), tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        temptear = Instantiate(tear);
        temptear.Init(tear.Owner, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(-randomX, randomY), tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        temptear = Instantiate(tear);
        temptear.Init(tear.Owner, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(randomX, -randomY), tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        temptear = Instantiate(tear);
        temptear.Init(tear.Owner, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(-randomX, -randomY), tear.IsParbolic);
    }

    public override void OnAbility(Player player)
	{
		EventManager.RegisterListener<TearDropEvent>(Division, 1);
		EventManager.RegisterListener<TearHitObstacleEvent>(Division, 1);
	}

	public override void RemoveSkill()
	{
		EventManager.UnregisterListener<TearDropEvent>(Division);
		EventManager.UnregisterListener<TearHitObstacleEvent>(Division);
	}
}