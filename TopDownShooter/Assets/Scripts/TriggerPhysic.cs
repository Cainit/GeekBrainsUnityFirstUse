using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhysic : Trigger
{
    [SerializeField]
    float minimumWeight;

    float currentWeight;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void CheckTrigger()
    {
        onTrigger = currentWeight >= minimumWeight ? true : false;

        Light[] lights = GetComponentsInChildren<Light>();

        if (onTrigger)
            foreach (Light light in lights) light.color = Color.green;
        else
            foreach (Light light in lights) light.color = Color.red;
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rbOther = other.GetComponent<Rigidbody>();
        if(rbOther)
        {
            currentWeight += rbOther.mass;
        }
        CheckTrigger();
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rbOther = other.GetComponent<Rigidbody>();
        if (rbOther)
        {
            currentWeight -= rbOther.mass;
        }
        CheckTrigger();
    }
}
