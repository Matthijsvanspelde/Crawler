using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class FollowPlayer : AIState
{
    public UnityEvent InAttackRange = new UnityEvent();
    Transform player;
    private EnemyStats enemyStats;
    private bool firstime = true;

    private void Awake()
    {
        player = Movement.instance.transform;
    }

    public override void AwakeState(NavMeshAgent agent)
    {
        enemyStats = (EnemyStats)Stats;
    }

    public override void HandleState(NavMeshAgent agent)
    {
        StartCoroutine(MoveTowardsPlayerPos(agent));
    }

    private IEnumerator MoveTowardsPlayerPos(NavMeshAgent agent)
    {
        SetAgentDestination(agent);

        while (agent.hasPath || Done == false)
        {
            SetAgentDestination(agent);
            SetAnimation();

            yield return new WaitForSeconds(Time.deltaTime);

            if (Vector3.Distance(player.transform.position, agent.transform.position) <= enemyStats.EnemyWeapon.Stats.AttackRange.GetValue())
            {
                StopAllCoroutines();
                firstime = true;
                agent.isStopped = true;
                InAttackRange.Invoke();
            }
        }
    }

    private void SetAnimation()
    {
        if (firstime)
        {
            enemyStats.animator.SetWalkingBool(true);
            enemyStats.animator.SetIdleBool(false);
            firstime = false;
        }
    }

    private void SetAgentDestination(NavMeshAgent agent)
    {
        agent.SetDestination(player.position);
    }

    public override void EndState(NavMeshAgent agent)
    {

    }
}
