using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class CheckForEnemy : AIState
{
    [SerializeField] private UnityEvent AfterFindingPlayer = new UnityEvent();
    private SphereCollider triggerCollider;
    private EnemyStats enemyStats;

    private void Start()
    {
        AwakeState();

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.LookAt(other.transform);

            if (SeePlayer(transform.forward))
            {
                Debug.Log("I see you");
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
