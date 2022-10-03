using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] AudioMixer audioMixer;

    private AudioSource musicSource;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayMusic();
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Lerp(-40f, -20f, volume));
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Lerp(-20f, 0f, volume));
    }

    public void PlayMusic()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.Play();
    }

    public void CheckMusicTemp(float percentHealth)
    {
        if (percentHealth < 0.5f)
            musicSource.pitch = 2f;
        else
            musicSource.pitch = 1f;
    }
}
