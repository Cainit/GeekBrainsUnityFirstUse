using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Interactable
{
    public float heal;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && PlayerController.Instance.GetComponent<Health>().IsNeedHeal())
        {
            PlayerController.Instance.GetComponent<Health>().Heal(heal);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            PlayGetUp();
            Destroy(this.gameObject, 3.0f);
        }
    }
}
