using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : UnitController
{
    //Player variables
    public Gun gun;
    Animator animator;
    //Rigidbody rb;
    NavMeshAgent agentNav;
    
    bool attackReady = true;
    
    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        
        GetComponent<Health>().OnDamage += OnHit;
        animator = GetComponent<Animator>();
        agentNav = GetComponent<NavMeshAgent>();
        
    }



    void Update()
    {
        if (PlayerController.Instance == null || !GetComponent<Health>().IsLive())
            return;

        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Attack");

        float distance = GetDistToPlayer();

        if (distance > distantionAttack && distance > distantionAggro) Patrol();
        if (distance > distantionAttack && distance <= distantionAggro) Aggro();
        if (distance <= distantionAttack) StartAttack();

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            agentNav.isStopped = true;
        else
            agentNav.isStopped = false;

        float velocity = agentNav.velocity.magnitude / agentNav.speed;
        animator.SetFloat("Speed", velocity);
    }

    void FixedUpdate()
    {
        if (PlayerController.Instance == null || !GetComponent<Health>().IsLive())
            return;
        

    }

    void Patrol()
    {
        agentNav.SetDestination(transform.position);
    }

    void Aggro()
    {
        agentNav.SetDestination(PlayerController.Instance.transform.position);
    }

    void StartAttack()
    {
        agentNav.SetDestination(transform.position);
        transform.LookAt(new Vector3(PlayerController.Instance.transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z));
        if (attackReady && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackReady = false;
            animator.SetTrigger("Attack");
            PlaySoundAttack();
            Invoke("EndAttack", attackReadyTime);
           
        }
    }

    void EndAttack()
    {
        attackReady = true;
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
        //rb.detectCollisions = false;
        //rb.isKinematic = true;
        agentNav.isStopped = true;

        Destroy(this.gameObject, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distantionAttack);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distantionAggro);
    }
}
