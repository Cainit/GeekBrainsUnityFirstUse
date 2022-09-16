using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Interactable
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            PlayerController.Instance.GetComponent<Health>().Damage(damage);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            PlayGetUp();
            Destroy(this.gameObject, 3.0f);
        }
    }
}
