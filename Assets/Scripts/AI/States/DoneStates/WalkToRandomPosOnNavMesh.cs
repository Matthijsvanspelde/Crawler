using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToRandomPosOnNavMesh : AIState
{
    [SerializeField] float RandomPosRadius = 0;
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
            if (!RepeatAtEndPoint)
            {
                if (agent.hasPath)
                {
                    Done = true;
                }
            }
            else
            {
                SetRandomDestinationOfAgent(agent);
            }
            yield return Time.deltaTime;
        }
    }
    
    private void SetRandomDestinationOfAgent(NavMeshAgent agent)
    {
        Vector3 randomPosOnNavmesh = GetRandomPosOnNavMesh();

        agent.SetDestination(randomPosOnNavmesh);
    }

    //Code copy from following link: https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html
    private Vector3 GetRandomPosOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * RandomPosRadius;
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
    }
}   
