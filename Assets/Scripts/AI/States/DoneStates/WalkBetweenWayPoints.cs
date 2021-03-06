﻿using System.Collections;
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
        while (!Done)
        {
            foreach (Transform wayPoint in WayPoints)
            {
                if (!ResolveInRandomOrder)
                {
                    agent.destination = wayPoint.position;
                }
                else
                {
                    agent.destination = WayPoints[Random.Range(0, WayPoints.Count)].transform.position;
                }

                yield return new WaitForSeconds(0.5f);

                while (agent.hasPath)
                {
                    yield return null;
                }

                setIdleAnimation();
                yield return new WaitForSeconds(WaitTimeAtPoint);
                setWalkingAnimation();
            }

            if (!LoopPath)
            {
                Done = true;
            }
        }    
    }

    private void setIdleAnimation()
    {
        enemyStats.animator.SetWalkingBool(false);
        enemyStats.animator.SetIdleBool(true);
    }

    private void setWalkingAnimation()
    {
        enemyStats.animator.SetIdleBool(false);
        enemyStats.animator.SetWalkingBool(true);
    }

    public override void EndState(NavMeshAgent agent)
    {
        base.EndState(agent);
        StopAllCoroutines();
    }
}
