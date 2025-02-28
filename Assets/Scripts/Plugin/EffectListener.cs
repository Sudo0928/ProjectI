using UnityEngine;

public class EffectListener : MonoBehaviour
{
    public GameObject LbloodPrefab;
    public GameObject SbloodPrefab;
    public GameObject Effect_fart;
    public GameObject TearsPop;

    public void OnEnable()
    {
        EventManager.RegisterListener<ExplosionEvent>(CreateEffect_fart);
        EventManager.RegisterListener<TearDropEvent>(CreateTearsPop);
        EventManager.RegisterListener<TearHitObstacleEvent>(CreateTearsPop);
        EventManager.RegisterListener<MonsterDeadEvent>(CreateSBloodEffect);
    }

    public void OnDisable()
    {
        EventManager.UnregisterListener<ExplosionEvent>(CreateEffect_fart);
        EventManager.UnregisterListener<TearDropEvent>(CreateTearsPop);
        EventManager.UnregisterListener<TearHitObstacleEvent>(CreateTearsPop);
        EventManager.UnregisterListener<MonsterDeadEvent>(CreateSBloodEffect);
    }

    private void CreateLBloodEffect()
    {

    }

    public void CreateSBloodEffect(MonsterDeadEvent e)
    {
        GameObject obj = Instantiate(SbloodPrefab, e.enemy.transform.position, Quaternion.identity);

        GameManager.Instance.SetTimer(() => Destroy(obj), 0.5f);
    }

    public void CreateEffect_fart(ExplosionEvent e)
    {
        GameObject obj = Instantiate(Effect_fart, e.position, Quaternion.identity);

        GameManager.Instance.SetTimer(() => Destroy(obj), 0.5f);
    }

    public void CreateTearsPop(TearDropEvent e)
    {
        GameObject obj = Instantiate(TearsPop, e.tear.transform.position, Quaternion.identity);

        GameManager.Instance.SetTimer(() => Destroy(obj), 0.5f);
    }

    public void CreateTearsPop(TearHitObstacleEvent e)
    {
        GameObject obj = Instantiate(TearsPop, e.tear.transform.position, Quaternion.identity);

        GameManager.Instance.SetTimer(() => Destroy(obj), 0.5f);
    }
}
