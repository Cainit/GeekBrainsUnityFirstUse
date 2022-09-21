using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected bool onTrigger { get; set; }

    public bool GetTrigger()
    {
        return onTrigger;
    }

}
