using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StayInPlace : State
{
    public override void AwakeState(NavMeshAgent agent)
    {

    }

    public override void EndState(NavMeshAgent agent)
    {

    }

    public override void HandleState(NavMeshAgent agent)
    {
        agent.SetDestination(transform.position += Vector3.forward);

        AtDestination(agent);
    }

    IEnumerator AtDestination(NavMeshAgent agent)
    {
        bool done = false;
        bool forward = false;

        while (done)
        {
            yield return null;
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                TurnDestinationAround(agent,forward);
            }
        }
    }

    private void TurnDestinationAround(NavMeshAgent agent, bool forward)
    {
        if (forward)
        {
            agent.SetDestination(transform.position += Vector3.forward * 10);
            forward = false;
        }
        else
        {
            agent.SetDestination(transform.position += Vector3.back *  10);
            forward = true;
        }
    }
}
