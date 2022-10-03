using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Interactable
{
    public float damage;
    [SerializeField]
    GameObject explosionPref;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Physic")
        {
            GameObject expl = Instantiate(explosionPref, transform.position, Quaternion.identity, transform.parent);
            Destroy(expl, 2f);

            if(other.GetComponent<Health>() != null)
                other.GetComponent<Health>().Damage(damage);

            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            PlayGetUp();
            Destroy(this.gameObject, 3.0f);
        }
    }
}
