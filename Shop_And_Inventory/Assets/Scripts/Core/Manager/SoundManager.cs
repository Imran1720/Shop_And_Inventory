using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioSource_BG;
    [SerializeField] private AudioSource audioSource_SFX;

    [Header("Audio Clips")]
    [SerializeField] private SoundType[] soundClips;

    private void Start() => PlayBGM(Sounds.BGM);

    private void PlayBGM(Sounds _bgm)
    {
        AudioClip clip = GetClip(_bgm);
        if (clip != null)
        {
            audioSource_BG.clip = clip;
            audioSource_BG.Play();
        }
    }

    public void PlaySoundFX(Sounds _sfx)
    {
        AudioClip clip = GetClip(_sfx);
        if (clip != null)
        {
            audioSource_SFX.PlayOneShot(clip);
        }
    }

    private AudioClip GetClip(Sounds bgm)
    {
        foreach (SoundType type in soundClips)
        {
            if (type.soundType == bgm) return type.soundClip;
        }
        return null;
    }
}




