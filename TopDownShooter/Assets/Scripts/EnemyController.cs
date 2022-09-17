using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : UnitController
{
    public Gun gun;
    Animator animator;
    NavMeshAgent agentNav;
    Transform currentPatrolPoint;

    bool attackReady = true;

    void Awake()
    {
        GetComponent<Health>().OnDamage += OnHit;
        animator = GetComponent<Animator>();
        agentNav = GetComponent<NavMeshAgent>();
        if(patrolPoints.Count == 1)
        {
            GameObject newWayPoint = new GameObject();
            newWayPoint.tag = "Waypoint";
            newWayPoint.transform.position = transform.position;
            patrolPoints.Add(newWayPoint.transform);
        }
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

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (currentPatrolPoint == null)
        {
            GetNextPatrolPoint();
        }
        else
        {
            Vector3 distanceToWalkPoint = transform.position - currentPatrolPoint.position;
            if (distanceToWalkPoint.magnitude < 1f)
            {
                GetNextPatrolPoint();
            }
        }


        if (currentPatrolPoint != null)
            agentNav.SetDestination(currentPatrolPoint.position);
        else
            agentNav.SetDestination(transform.position);
    }

    Transform GetNextPatrolPoint()
    {
        if (patrolPoints.Count <= 1)
            return null;

        if (currentPatrolPoint == null)
            currentPatrolPoint = patrolPoints[0];
        else
        {
            int i = patrolPoints.IndexOf(currentPatrolPoint);
            if (i + 1 < patrolPoints.Count)
                currentPatrolPoint = patrolPoints[i + 1];
            else
                currentPatrolPoint = patrolPoints[0];
        }

        return currentPatrolPoint;
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
