using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class LookForPlayer : AIState
{
    [SerializeField] private UnityEvent PlayerSpotted = new UnityEvent();
    private SphereCollider triggerCollider;
    private EnemyStats enemyStats;

    private bool lookForPlayer = false;

    private void Start()
    {
        AwakeState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (lookForPlayer)
        {
            if (other.tag == "Player")
            {
                Quaternion oldRotation = transform.rotation;
                transform.LookAt(other.transform);
                transform.rotation = oldRotation;

                if (SeePlayer(transform.forward))
                {
                    PlayerSpotted.Invoke();
                    lookForPlayer = false;
                }
            }
        }
    }

    private bool SeePlayer(Vector3 playerPos)
    {
        Ray ray = new Ray(transform.position, playerPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, enemyStats.DetectionRadius))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 5f);

            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    public void AwakeState()
    {
        SetTrigger();
        lookForPlayer = true;
    }

    private void SetTrigger()
    {
        enemyStats = (EnemyStats)Stats.Stats;
        triggerCollider = GetComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = enemyStats.DetectionRadius;
    }

    private void OnDrawGizmos()
    {
        if (enemyStats != null)
        {
            Gizmos.DrawWireSphere(transform.position, enemyStats.DetectionRadius);
        }
    }
}
