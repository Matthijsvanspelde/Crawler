using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class LookForPlayer : AIState
{
    [SerializeField] private UnityEvent PlayerSpotted = new UnityEvent();
    [Range(1,360)]
    [SerializeField] private float ViewAngle = 70;
    [Range(1, 360)]
    [SerializeField] private float detectionRadius = 10;

    private SphereCollider triggerCollider;
    

    private bool lookForPlayer = false;

    private void Start()
    {
        AwakeState();
    }

    public override void AwakeState(NavMeshAgent agent)
    {
        AwakeState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (lookForPlayer)
        {
            if (other.tag == "Player")
            {
                Vector3 rayEndPoint = other.transform.position - transform.position;
                float angle = Vector3.Angle(rayEndPoint, transform.forward);
                if (angle < ViewAngle - 30)
                {
                    if (SeePlayer(rayEndPoint))
                    {
                        PlayerSpotted.Invoke();
                        lookForPlayer = false;
                    }
                }
            }
        }
    }

    private bool SeePlayer(Vector3 playerPos)
    {
        Ray ray = new Ray(transform.position, playerPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, detectionRadius))
        {
            Debug.DrawRay(transform.position, playerPos, Color.green, 5f);

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
        Done = false;
    }

    private void SetTrigger()
    {
        triggerCollider = GetComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = detectionRadius;
    }

    private void OnDrawGizmos()
    {
            float totalFOV = ViewAngle;
            float rayRange = detectionRadius;
            float halfFOV = totalFOV / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * rayRange) ;
            Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
            //Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
