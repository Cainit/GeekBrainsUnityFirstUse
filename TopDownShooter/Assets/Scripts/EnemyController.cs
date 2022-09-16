using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : UnitController
{
    //Player variables
    public Gun gun;
    Animator animator;
    Rigidbody rb;

    Vector3 moveDirection = new Vector3();
    Vector3 targetPos = new Vector3();
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        GetComponent<Health>().OnDamage += OnHit;
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        if (PlayerController.Instance == null)
            return;

        targetPos.x = PlayerController.Instance.transform.position.x;
        targetPos.y = transform.position.y;
        targetPos.z = PlayerController.Instance.transform.position.z;

        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    void FixedUpdate()
    {
        if (PlayerController.Instance == null || !GetComponent<Health>().IsLive())
            return;

        float distance = GetDistToPlayer();

        if (distance > distantionAttack)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                return;
            rb.velocity = transform.forward * speedMove;
        }

        transform.LookAt(targetPos);

        if(distance <= distantionAttack)
            animator.SetTrigger("Attack");
        else
            animator.ResetTrigger("Attack");

        animator.ResetTrigger("Hit");
    }

    override public void OnHit(object sender, EventArgs args)
    {
        animator.SetTrigger("Hit");

        PlaySoundHit();

        if (!GetComponent<Health>().IsLive())
            Death();
    }

    override public void Attack()
    {
        animator.ResetTrigger("Attack");
        if(GetDistToPlayer() < distantionAttack)
        {
            PlayerController.Instance.GetComponent<Health>().Damage(damage);
        }
    }

    override public void Death()
    {
        PlaySoundDeath();
        animator.SetBool("Death", true);
        GetComponent<Collider>().enabled = false;
        rb.detectCollisions = false;
        rb.isKinematic = true;

        Destroy(this.gameObject, 5f);
    }
}
