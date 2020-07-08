using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterContainerTile : Tile
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemyStateMachine;
    [SerializeField] private List<Transform> WayPoints = new List<Transform>();

    private StateMachine enemyOnTile = null;

    public override void InitializeTile()
    {
        base.InitializeTile();
        SpawnEnemyOnTile();
        SetWayPointsOfEnemyOnTile();
    }

    private void SpawnEnemyOnTile()
    {
        enemyOnTile = Instantiate(enemyStateMachine).GetComponentInChildren<StateMachine>();

        enemyOnTile.agent.Warp(spawnPoint.position);
    }

    private void SetWayPointsOfEnemyOnTile()
    {
        if (WayPoints.Count > 0)
        {
            if (enemyOnTile.defaultState.GetType() == typeof(WalkBetweenWayPoints))
            {
                WalkBetweenWayPoints waypointContainer = (WalkBetweenWayPoints)enemyOnTile.defaultState;
                waypointContainer.SetWayPointLocations(WayPoints);
            }
        }
    }

    public StateMachine EnemyOnTile { get => enemyOnTile; }
}
