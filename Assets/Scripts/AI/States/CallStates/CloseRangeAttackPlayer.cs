using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CloseRangeAttackPlayer : AIState
{
    public UnityEvent EndOfAttack = new UnityEvent();
    private EnemyStats enemyStats;

    public override void AwakeState(NavMeshAgent agent)
    {
        base.AwakeState(agent);
        agent.ResetPath();
        enemyStats = (EnemyStats)Stats;
    }

    public override void HandleState(NavMeshAgent agent)
    {
        base.HandleState(agent);
        enemyStats.EnemyWeapon.HandleAttack();
        enemyStats.animator.SetIdleBool(true);
        enemyStats.animator.SetWalkingBool(false);
        StartCoroutine(SetTrue());
    }

    IEnumerator SetTrue()
    {
        yield return new WaitForSeconds(2f);
        EndOfAttack.Invoke();
    }

    public override void EndState(NavMeshAgent agent)
    {
        agent.isStopped = false;
    }
}
