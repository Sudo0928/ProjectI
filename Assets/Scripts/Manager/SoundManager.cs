using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();

       _audioSource =  gameObject.GetOrAddComponent<AudioSource>();
    }

    public void ChangeBGM(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
}
