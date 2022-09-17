using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitController : MonoBehaviour
{
    public float speedMove;
    public float distantionAggro;
    public float distantionAttack;
    public float damage;
    public float attackReadyTime = 0.8f;

    [SerializeField] List<AudioClip> soundsAggro;
    [SerializeField] List<AudioClip> soundsAttack;
    [SerializeField] List<AudioClip> soundsHit;
    [SerializeField] List<AudioClip> soundsDeath;

    public float GetDistToPlayer()
    {
        return Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
    }

    virtual public void OnHit(object sender, EventArgs args)
    {
        if (!GetComponent<Health>().IsLive())
            Death();
    }

    virtual public void Death()
    {
        Destroy(this.gameObject);
    }

    virtual public void Attack()
    {
        
        print("attack!");
    }

    public void PlaySoundHit()
    {
        PlaySoundRandom(soundsHit);
    }

    public void PlaySoundDeath()
    {
        PlaySoundRandom(soundsDeath);
    }

    public void PlaySoundAttack()
    {
        PlaySoundRandom(soundsAttack);
    }

    public void PlaySoundAggro()
    {
        PlaySoundRandom(soundsAggro);
    }

    public void PlaySoundRandom(List<AudioClip> listSounds)
    {
        if (listSounds.Count < 1)
            return;

        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(listSounds[UnityEngine.Random.Range(0, listSounds.Count)]);
    }
}
