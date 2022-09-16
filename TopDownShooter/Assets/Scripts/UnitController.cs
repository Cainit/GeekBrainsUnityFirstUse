using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitController : MonoBehaviour
{
    public float speedMove;
    public float distantionAttack;
    public float damage;

    public float GetDistToPlayer()
    {
        return Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
    }

    virtual public void OnHit(object sender, EventArgs args)
    {
        if (!GetComponent<Health>().IsLive())
            Death();
    }

    virtual public void Death()
    {
        Destroy(this.gameObject);
    }

    virtual public void Attack()
    {
        
        print("attack!");
    }
}
