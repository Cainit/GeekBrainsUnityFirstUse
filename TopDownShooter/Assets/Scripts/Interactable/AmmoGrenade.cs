using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoGrenade : Interactable
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.AddGrenades(3);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            PlayGetUp();
            Destroy(this.gameObject, 3.0f);
        }
    }
}
