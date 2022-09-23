using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bulletPref;
    public bool isFiring;

    public float fireRate;
    private float fireTimer;

    public Transform firePoint;
    [SerializeField]
    public List<AudioClip> shootSounds = new List<AudioClip>();


    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            fireTimer += Time.deltaTime * fireRate;

            if (fireTimer >= 1)
            {
                fireTimer = 0;
                Shoot();
                //newBullet.speed = 15;
            }
        }
        else
        {
            fireTimer = 1;
        }
    }

    public void Shoot()
    {
        Bullet newBullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
        PlaySoundRandom(shootSounds);
    }

    public void PlaySoundRandom(List<AudioClip> listSounds)
    {
        if (listSounds.Count < 1)
            return;

        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(listSounds[UnityEngine.Random.Range(0, listSounds.Count)]);
    }
}
