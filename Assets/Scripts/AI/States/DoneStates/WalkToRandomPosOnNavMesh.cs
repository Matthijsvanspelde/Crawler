using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToRandomPosOnNavMesh : AIState
{
    [SerializeField] float RandomPosRadius = 0;
    [SerializeField] float TimeToWaitAtPoint;
    [Range(0,1)]
    [SerializeField] float MinDistance;

    [SerializeField] bool RepeatAtEndPoint;

    public override void HandleState(NavMeshAgent agent)
    {
        base.HandleState(agent);

        SetRandomDestinationOfAgent(agent);

        StartCoroutine(CheckIfDone(agent));
    }

    private IEnumerator CheckIfDone(NavMeshAgent agent)
    {
        while (!Done)
        {
            if (!agent.hasPath)
            {
                if (!RepeatAtEndPoint)
                {
                    Done = true;
                }
                else
                {
                    yield return new WaitForSeconds(TimeToWaitAtPoint);
                    SetRandomDestinationOfAgent(agent);
                }
            }
            yield return Time.deltaTime;
        }
        StopAllCoroutines();
    }
    
    private void SetRandomDestinationOfAgent(NavMeshAgent agent)
    {
        Vector3 FinalPosition = transform.position;

        while (Vector3.Distance(transform.position, FinalPosition) < MinDistance)
        {
            FinalPosition = GetRandomPosOnNavMesh();
        }

        agent.SetDestination(FinalPosition);
    }

    //Code copy from following link: https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html
    private Vector3 GetRandomPosOnNavMesh()
    {
        Vector3 random = Random.insideUnitSphere;
        Vector3 randomDirection = random * RandomPosRadius;
        
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, RandomPosRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RandomPosRadius);
        Gizmos.DrawWireSphere(transform.position, MinDistance * RandomPosRadius);
    }
}   
