using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerMessage : MonoBehaviour
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
            GameManager.Instance.ShowMessage(caption, text);
        }
    }
}
