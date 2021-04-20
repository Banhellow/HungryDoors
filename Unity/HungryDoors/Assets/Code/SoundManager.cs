using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX { SwordAttack, AxeAttack, Damage, Throw, Eating, ItemDrop, Death, ItemPickup, CrossbowAtack, ItemHit, ItemBreaks }

[Serializable]
public struct SfxData
{
    public SFX sfx;
    public AudioClip[] clips;
}


public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicAS;
    public bool musicIsOn = true;
    [Space(10)]
    public AudioClip MusicClip;

    [Header("Sfx")]
    public AudioSource sfxAS;
    public bool sfxIsOn = true;
    public List<SfxData> sfxClipList;

    void Start()
    {
        if (musicIsOn)
        {
            musicAS.clip = MusicClip;
            musicAS.Play();
        }
    }

    public void PauseMusic()
    {
        musicAS.volume = 0;
    }

    public void ResumeMusic()
    {
        musicAS.volume = 1;
    }

    public void PlayStopMusicState()
    {
        //musicIsOn = !musicIsOn;

        if (musicIsOn)
            musicAS.Play();
        else
            musicAS.Stop();
    }

    public bool SfxCheck()
    {
        return sfxIsOn;
    }

    public void PlaySfx(SFX s)
    {
        var sfx = GetSFX(s);
        if (sfxIsOn && sfx != null)
            sfxAS.PlayOneShot(sfx);
    }

    public void PlaySfxWithDelay(SFX s, float delay)
    {
        var sfx = GetSFX(s);
        if (sfxIsOn && sfx != null)
            DOVirtual.DelayedCall(delay, () => sfxAS.PlayOneShot(sfx));
    }

    private AudioClip GetSFX(SFX s)
    {
        for (int i = 0; i < sfxClipList.Count; i++)
        {
            if (s == sfxClipList[i].sfx)
            {
                if (sfxClipList[i].clips.Length == 1)
                {
                    return sfxClipList[i].clips[0];
                }
                else
                {
                    return sfxClipList[i].clips[UnityEngine.Random.Range(0, sfxClipList[i].clips.Length - 1)];
                }
            }
        }

        return null;
    }
}
