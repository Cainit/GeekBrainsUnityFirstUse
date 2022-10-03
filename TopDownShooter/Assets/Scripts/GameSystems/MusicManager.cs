using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] AudioMixer audioMixer;
    
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
        audioMixer.SetFloat("Music", Mathf.Lerp(-20f, 0f, volume));
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Lerp(-20f, 0f, volume));
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }
}
