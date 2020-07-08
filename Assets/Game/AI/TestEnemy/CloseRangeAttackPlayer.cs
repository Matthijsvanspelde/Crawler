using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class CloseRangeAttackPlayer : AIState
{
    private EnemyStats enemyStats;

    public override void AwakeState(NavMeshAgent agent)
    {
        base.AwakeState(agent);
        agent.isStopped = true;
        enemyStats = (EnemyStats)Stats;
    }

    public override void HandleState(NavMeshAgent agent)
    {
        base.HandleState(agent);
        enemyStats.EnemyWeapon.HandleAttack();
    }

    public override void EndState(NavMeshAgent agent)
    {
        base.EndState(agent);
        agent.isStopped = false;
    }
}
