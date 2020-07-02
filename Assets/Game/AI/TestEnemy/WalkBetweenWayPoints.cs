using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBetweenWayPoints : AIState
{
    [SerializeField]private List<Transform> WayPoints = new List<Transform>();
    [SerializeField] private bool LoopPath = false;
    [Range(0.5f,10)]
    [SerializeField] private float WaitTimeAtPoint;

    public override void AwakeState(NavMeshAgent agent)
    {

    }

    public override void HandleState(NavMeshAgent agent)
    {
        StartCoroutine(MoveToWayPoint(agent));
    }

    private IEnumerator MoveToWayPoint(NavMeshAgent agent)
    {
        
            foreach (Transform wayPoint in WayPoints)
            {
                agent.destination = wayPoint.position;

                yield return new WaitForSeconds(0.5f);

                while (agent.hasPath)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(WaitTimeAtPoint);
            }

        if (LoopPath)
        {
            HandleState(agent);
        }
        else
        {
            Done = true;
        }
    }

    public override void EndState(NavMeshAgent agent)
    {

    }

    
}
