using UnityEngine;

public class CricketsBody : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.RegisterListener<TearDestroyEvent>(Division, 1);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<TearDestroyEvent>(Division);
    }

    private void Division(TearDestroyEvent e)
    {
        if (e.tear.Size < 0.5f) return;

        BaseTear tear;

        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        tear =Instantiate(e.tear);
        tear.Init(e.tear.Owner, e.tear.Speed, e.tear.Distance, e.tear.Size * 0.5f, new Vector2(randomX, randomY), e.tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        tear = Instantiate(e.tear);
        tear.Init(e.tear.Owner, e.tear.Speed, e.tear.Distance, e.tear.Size * 0.5f, new Vector2(-randomX, randomY), e.tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        tear = Instantiate(e.tear);
        tear.Init(e.tear.Owner, e.tear.Speed, e.tear.Distance, e.tear.Size * 0.5f, new Vector2(randomX, -randomY), e.tear.IsParbolic);

        randomX = Random.Range(0f, 1f);
        randomY = Random.Range(0f, 1f);

        tear = Instantiate(e.tear);
        tear.Init(e.tear.Owner, e.tear.Speed, e.tear.Distance, e.tear.Size * 0.5f, new Vector2(-randomX, -randomY), e.tear.IsParbolic);
    }
}