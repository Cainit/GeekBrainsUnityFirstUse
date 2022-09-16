using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);     
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
