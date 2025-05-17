using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSourcePrefab;

    [Header("Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private List<AudioSource> activeSFX = new List<AudioSource>();

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
    }

    // Play Music
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    // Pause Music
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    // Resume Music
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // Stop Music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Set Music Volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }

    // Play Sound Effect
    public void PlaySFX(AudioClip clip, bool loop)
    {
        if (clip == null)
        {
            return;
        }

        AudioSource sfx = Instantiate(sfxSourcePrefab, transform);
        sfx.clip = clip;
        sfx.volume = sfxVolume;
        sfx.loop = loop;
        sfx.Play();
        activeSFX.Add(sfx);

        if (!loop)
        {
            StartCoroutine(DestroyAfterPlay(sfx));
        }
    }

    // Set SFX Volume
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    // Stop SFX
    public void StopSFX(AudioClip audioClip)
    {
        foreach (AudioSource sfx in activeSFX)
        {
            if (sfx.clip == audioClip)
            {
                sfx.Stop();
            } 
        }
    }

    // Stop All SFX
    public void StopAllSFX()
    {
        foreach (var sfx in activeSFX)
        {
            if (sfx != null)
            {
                sfx.Stop();
            }
        }
    }

    // Coroutine to clean up
    private System.Collections.IEnumerator DestroyAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        activeSFX.Remove(source);
        Destroy(source.gameObject);
    }
}

