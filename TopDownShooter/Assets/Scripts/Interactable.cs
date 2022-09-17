using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public AudioClip getUpSound;

    public void PlayGetUp()
    {
        if(getUpSound)
            PlaySound(getUpSound);
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(clip);
    }

    public void PlaySoundRandom(List<AudioClip> listSounds)
    {
        if (listSounds.Count < 1)
            return;

        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(listSounds[UnityEngine.Random.Range(0, listSounds.Count)]);
    }
}
