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
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0)
            {
                fireTimer = fireRate;
                Bullet newBullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
                //newBullet.speed = 15;
            }
        }
        else
        {
            fireTimer = 0;
        }


    }
}
