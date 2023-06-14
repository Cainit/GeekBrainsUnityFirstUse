using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerMessage : Trigger
{
    [SerializeField]
    string caption = "";
    [SerializeField]
    string text = "";

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (oneShot)
                GetComponent<Collider>().enabled = false;

            GameManager.Instance.ShowMessage(caption, text);
        }
    }
}
