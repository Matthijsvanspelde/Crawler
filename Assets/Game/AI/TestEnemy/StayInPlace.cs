using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StayInPlace : AIState
{
    private bool forward = false;
    private Vector3 pos;

    public override void AwakeState(NavMeshAgent agent)
    {

    }


    public override void EndState(NavMeshAgent agent)
    {

    }

    public override void HandleState(NavMeshAgent agent)
    {
        // agent.SetDestination(transform.position + Vector3.forward * 100);
        pos = transform.position;
        agent.destination = transform.forward;
        agent.isStopped = false;

        StartCoroutine(AtDestination(agent));
    }

    IEnumerator AtDestination(NavMeshAgent agent)
    {
        bool done = false;
        
        while (!done)
        {
            yield return new WaitForSeconds(Random.Range(0,2));
            if (!agent.hasPath)
            {
                TurnDestinationAround(agent);
            }
        }
    }

    private void TurnDestinationAround(NavMeshAgent agent)
    {
        if (forward)
        {
            if(Random.Range(0,2) == 1)
            {
                agent.destination = transform.right;
            }
            else
            {
                agent.destination = transform.forward;
            }

            Done = true;
        }
        else
        {
            agent.destination = pos;

            forward = true;
        }
    }
}
