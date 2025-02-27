using UnityEngine;

public class SoundListener : MonoBehaviour
{
    public AudioClip[] explosionSounds;

    public AudioClip[] tearLunchSounds;
    public AudioClip tearDropSound;
    public AudioClip[] tearHitSounds;

    public AudioClip[] playerDeadSounds;
    public AudioClip[] playerDamagedSounds;

    private void OnEnable()
    {
        EventManager.RegisterListener<ExplosionEvent>(ExplosionSound);

        EventManager.RegisterListener<TearLaunchEvent>(TearLunchSound);
        EventManager.RegisterListener<TearDropEvent>(TearDropSound);
        EventManager.RegisterListener<TearHitObstacleEvent>(TearDropSound);
        EventManager.RegisterListener<TearHitEntityEvent>(TearHitSound);

        EventManager.RegisterListener<PlayerDamagedEvent>(PlayerDamagedSound);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<ExplosionEvent>(ExplosionSound);

        EventManager.UnregisterListener<TearLaunchEvent>(TearLunchSound);
        EventManager.UnregisterListener<TearDropEvent>(TearDropSound);
        EventManager.UnregisterListener<TearHitObstacleEvent>(TearDropSound);
        EventManager.UnregisterListener<TearHitEntityEvent>(TearHitSound);

        EventManager.UnregisterListener<PlayerDamagedEvent>(PlayerDamagedSound);
    }

    private void ExplosionSound(ExplosionEvent e)
    {
        int random = Random.Range(0, explosionSounds.Length);
        SoundManager.Instance.PlaySFX(explosionSounds[random]);
    }

    private void TearLunchSound(TearLaunchEvent e)
    {
        int random = Random.Range(0, tearLunchSounds.Length);
        SoundManager.Instance.PlaySFX(tearLunchSounds[random]);
    }

    private void TearDropSound(TearDropEvent e)
    {
        SoundManager.Instance.PlaySFX(tearDropSound);
    }

    private void TearDropSound(TearHitObstacleEvent e)
    {
        SoundManager.Instance.PlaySFX(tearDropSound);
    }

    private void TearShowerSound()
    {

    }

    private void PlayerDeadSound()
    {
        int random = Random.Range(0, playerDeadSounds.Length);
        SoundManager.Instance.PlaySFX(playerDeadSounds[random]);
    }

    private void PlayerDamagedSound(PlayerDamagedEvent e)
    {
        int random = Random.Range(0, playerDamagedSounds.Length);
        SoundManager.Instance.PlaySFX(playerDamagedSounds[random]);
    }

    

    private void TearHitSound(TearHitEntityEvent e)
    {
        int random = Random.Range(0, tearHitSounds.Length);
        SoundManager.Instance.PlaySFX(tearHitSounds[random]);
    }
}