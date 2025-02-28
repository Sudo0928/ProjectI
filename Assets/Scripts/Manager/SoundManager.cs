using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;
    AudioSource _bgAudio;

    protected override void Awake()
    {
        base.Awake();

       _audioSource =  gameObject.GetOrAnyComponent<AudioSource>();
		_bgAudio = gameObject.AddComponent<AudioSource>();
        _bgAudio.volume = 0.5f;
	}

    public void ChangeBGM(AudioClip clip)
    {
		_bgAudio.clip = clip;
        _bgAudio.Play();
    }

    public void PlaySFX(AudioClip clip, float pitch = 1, float voulme = 1)
    {
        _audioSource.pitch = pitch;
        _audioSource.volume = voulme;

        _audioSource.PlayOneShot(clip);
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public void Play()
    {
        _audioSource.Play();
    }
}
