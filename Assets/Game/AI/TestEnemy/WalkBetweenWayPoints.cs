using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBetweenWayPoints : AIState
{
    [SerializeField] private List<Transform> WayPoints = new List<Transform>();
    [SerializeField] private bool LoopPath = false;
    [SerializeField] private bool ResolveInRandomOrder = false;
    [Range(0.5f,10)]
    [SerializeField] private float WaitTimeAtPoint;

    private int currentIndex = 0;
    private EnemyStats enemyStats;

    public void SetWayPointLocations(List<Transform> waypointsToAdd)
    {
        foreach (Transform wayPoint in waypointsToAdd)
        {
            WayPoints.Add(wayPoint);
        }
    }

    public override void AwakeState(NavMeshAgent agent)
    {
        enemyStats = (EnemyStats)Stats;
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

            setIdleAnimation();
            yield return new WaitForSeconds(WaitTimeAtPoint);
            setWalkingAnimation();
        }

        if (LoopPath)
        {
            currentIndex = 0;
            HandleState(agent);
        }
        else
        {
            Done = true;
        }
    }

    private void setIdleAnimation()
    {
        enemyStats.Animator.SetIdleTrigger();
        enemyStats.Animator.SetWalkingBool(false);
        enemyStats.Animator.SetIdleBool(true);
    }

    private void setWalkingAnimation()
    {
        enemyStats.Animator.SetWalkingTrigger();
        enemyStats.Animator.SetWalkingBool(true);
        enemyStats.Animator.SetIdleBool(false);
    }

    public override void EndState(NavMeshAgent agent)
    {

    }
}
