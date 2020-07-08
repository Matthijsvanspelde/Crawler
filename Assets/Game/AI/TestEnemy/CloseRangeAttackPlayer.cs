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
        enemyStats.Animator.SetIdleBool(true);
        enemyStats.Animator.SetWalkingBool(false);
        StartCoroutine(SetTrue());
    }

    IEnumerator SetTrue()
    {
        yield return new WaitForSeconds(2f);
        EndOfAttack.Invoke();
    }

    public override void EndState(NavMeshAgent agent)
    {
        
    }
}
