using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitController : MonoBehaviour
{
    public float speedMove;
    public float distantionAttack;

    public float GetDistToPlayer()
    {
        return Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
    }

    virtual public void OnHit(object sender, EventArgs args)
    {

    }

    virtual public void Death()
    {

    }
}
