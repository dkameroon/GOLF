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
    [SerializeField] public AudioSource musicAudioSource;
    [SerializeField] public AudioSource soundEffectsAudioSource;

    public float volumeMusic;
    public float volumeSounds;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        volumeMusic = PlayerPrefs.GetFloat(PlayerPrefsNames.PLAYER_PREFS_NAMES_MUSIC_VOLUME, 0.5f);
        volumeSounds = PlayerPrefs.GetFloat(PlayerPrefsNames.PLAYER_PREFS_NAMES_SOUNDS_VOLUME, 0.5f);

        musicAudioSource.volume = volumeMusic;
        soundEffectsAudioSource.volume = volumeSounds;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip != null)
        {
            musicAudioSource.clip = musicClip;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        if (audioClipArray.Length > 0)
        {
            AudioClip clipToPlay = audioClipArray[Random.Range(0, audioClipArray.Length)];
            soundEffectsAudioSource.PlayOneShot(clipToPlay, volumeMultiplier * volumeSounds);
        }
    }

    public void PlayHitSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.hit, position, volume);
    }

    public void PlayPickUpSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.pickUpCoin, position, volume);
    }

    public void PlayDefeatSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.defeat, position, volume);
    }

    public void PlayWinSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.win, position, volume);
    }

    public void PlayButtonSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.buttonClick, position, volume);
    }

    public void PlayLevelUpSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.upgrade, position, volume);
    }

    public void PlayBuySomethingSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.buySomething, position, volume);
    }

    public void SetMusicVolume(float volume)
    {
        volumeMusic = volume;
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat(PlayerPrefsNames.PLAYER_PREFS_NAMES_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public void SetSoundEffectsVolume(float volume)
    {
        volumeSounds = volume;
        soundEffectsAudioSource.volume = volume;
        PlayerPrefs.SetFloat(PlayerPrefsNames.PLAYER_PREFS_NAMES_SOUNDS_VOLUME, volume);
        PlayerPrefs.Save();
    }
}
