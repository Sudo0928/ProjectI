using UnityEngine;

public class BurstingShots : SpecialAbility
{
    private BaseTear tear;
    private BaseTear prefab;

    private void OnEnable()
    {
        //EventManager.RegisterListener<TearDestroyEvent>(Division, 1);
    }

    private void OnDisable()
    {
        //EventManager.UnregisterListener<TearDestroyEvent>(Division);
    }

    public void Start()
    {
        prefab = GameManager.Instance.tear;
    }

    private void Division(TearDropEvent e)
    {
        this.tear = e.tear;

        Division();
    }

    private void Division(TearHitObstacleEvent e)
    {
        this.tear = e.tear;

        if (this.tear.Size < 0.5f) return;

        if (e.hitCollision.contactCount > 0)
        {
            Vector2 normal = e.hitCollision.contacts[0].normal;

            for (int i = 0; i < 4; i++)
            {
                float randomX = 0;
                float randomY = 0;

                randomX = randomX.GetRange(-1f, 1f);
                randomY = randomX.GetRange(-1f, 1f);

                randomX = normal.x != 0 ? normal.x * 0.5f : randomX;
                randomY = normal.y != 0 ? normal.y * 0.5f : randomY;


                Vector2 direction = new Vector2(randomX, randomY).normalized;

                Debug.Log(direction);

                BaseTear temptear = Instantiate(prefab, tear.transform.position, Quaternion.identity);
                temptear.Init(tear.Owner, tear.Damage * 0.5f, tear.Speed, tear.Distance * 0.5f, tear.Size * 0.5f, direction, tear.IsParbolic);
            }
        }
    }

    private void Division()
    {
        if (this.tear.Size < 0.9f) return;

        BaseTear temptear;

        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        temptear = Instantiate(prefab, tear.transform.position, Quaternion.identity);
        temptear.Init(tear.Owner, tear.Damage, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(randomX, randomY), tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        temptear = Instantiate(prefab, tear.transform.position, Quaternion.identity);
        temptear.Init(tear.Owner, tear.Damage, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(-randomX, randomY), tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        temptear = Instantiate(prefab, tear.transform.position, Quaternion.identity);
        temptear.Init(tear.Owner, tear.Damage, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(randomX, -randomY), tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        temptear = Instantiate(prefab, tear.transform.position, Quaternion.identity);
        temptear.Init(tear.Owner, tear.Damage, tear.Speed, tear.Distance, tear.Size * 0.5f, new Vector2(-randomX, -randomY), tear.IsParbolic);
    }

    public override void OnAbility(PlayerController player)
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