using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelController : UnitController
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Health>().OnDamage += OnHit;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance == null)
            return;

        //if(GetDistToPlayer() <= distantionAttack)
        transform.LookAt(new Vector3(PlayerController.Instance.transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z));
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
