using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TriggerDeath : Trigger
{
    void Start()
    {
        GetComponent<Health>().OnDamage += OnHit;
    }

    public void OnHit(object sender, EventArgs args)
    {
        if (!GetComponent<Health>().IsLive())
        {
            GameManager.Instance.Win();
            
        }
    }
}
