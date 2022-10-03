using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurrelController : UnitController
{
    [SerializeField]
    GameObject explosionPref;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Health>().OnDamage += OnHit;
    }

    override public void OnHit(object sender, EventArgs args)
    {
        if (!GetComponent<Health>().IsLive())
        {
            Death();
            GameObject expl = Instantiate(explosionPref, transform.position, Quaternion.identity, transform.parent);
            Destroy(expl, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance == null)
            return;

        //if(GetDistToPlayer() <= distantionAttack)
        GetComponentInChildren<Gun>().transform.LookAt(new Vector3(PlayerController.Instance.transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z));
    }

    
    void FixedUpdate()
    {
        if (PlayerController.Instance == null)
            return;

        if(GetDistToPlayer() <= distantionAttack && PlayerController.Instance.GetComponent<Health>().IsLive())
        {
            GetComponentInChildren<Gun>().isFiring = true;
        }
        else
        {
            GetComponentInChildren<Gun>().isFiring = false;
        }
    }
}
