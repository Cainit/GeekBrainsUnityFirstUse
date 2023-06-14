using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeed : Interactable
{
    public float bonusTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.AddBonusSpeed(bonusTime);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            PlayGetUp();
            Destroy(this.gameObject, 3.0f);
        }
    }
}
