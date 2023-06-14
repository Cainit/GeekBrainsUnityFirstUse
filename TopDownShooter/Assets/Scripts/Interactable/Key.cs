using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public int keyID;
    public GameObject keyPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.AddKey(keyID);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            PlayGetUp();
            KeyInventory.Instance.AddKey(keyPrefab, keyID);
            Destroy(this.gameObject, 3.0f);
        }
    }
}
