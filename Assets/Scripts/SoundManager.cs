using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    public AudioSource volumeMusic;
    public float volumeSounds;
    private void Awake()
    {
        Instance = this;
        volumeSounds = PlayerPrefs.GetFloat("SoundsVolume", 1f);
    }
    

    private void PlaySound(AudioClip[] audioClipArray,Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volumeMultiplier * volumeSounds);
    }

    public void PlayHitSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.hit,position, volume);
    }
    
    public void PlayPickUpSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.pickUpCoin,position, volume);
    }
    
    public void PlayDefeatSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.defeat,position, volume);
    }
    
    public void PlayWinSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.win,position, volume);
    }
    
    public void PlayLevelUpSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.upgrade,position, volume);
    }
    
    public void PlayBuySomethingSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.buySomething,position, volume);
    }
    

}
