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

    private float minimalAggroTime = 15f;
    private float currentAggroTime = 0;

    void Awake()
    {
        GetComponent<Health>().OnDamage += OnHit;
        animator = GetComponent<Animator>();
        agentNav = GetComponent<NavMeshAgent>();
        GetComponent<Rigidbody>().freezeRotation = true;
        if (patrolPoints.Count == 1)
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

        if (currentAggroTime >= 0)
            currentAggroTime -= 1.0f * Time.deltaTime;

        float distance = GetDistToPlayer();

        bool playerVisibility = CheckVisibleZone();

        if (distance > distantionAttack && (distance > distantionAggro || !playerVisibility) && currentAggroTime <= 0f) Patrol();
        if (distance > distantionAttack && (playerVisibility || currentAggroTime > 0f)) Aggro();
        if (distance <= distantionAttack && currentAggroTime > 0f) StartAttack();

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

    bool CheckVisibleZone()
    {
        if (GetDistToPlayer() <= distantionAggro)
        {
            Vector3 upHalf = new Vector3(0,0.5f,0);
            Ray rayHit = new Ray(transform.position + upHalf, (PlayerController.Instance.transform.position + upHalf) - (transform.position + upHalf));
            RaycastHit hitResult;
            if (Physics.Raycast(rayHit, out hitResult, distantionAggro))
            {
                if(hitResult.collider.gameObject == PlayerController.Instance.gameObject)
                {
                    if(Vector3.Angle(transform.forward, PlayerController.Instance.transform.position - transform.position) <= 70)
                        return true;
                }
            }
        }

        return false;
    }

    void Aggro()
    {
        agentNav.SetDestination(PlayerController.Instance.transform.position);
        if(currentAggroTime <= 0)
            currentAggroTime = minimalAggroTime;
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

        currentAggroTime = minimalAggroTime;

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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
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
