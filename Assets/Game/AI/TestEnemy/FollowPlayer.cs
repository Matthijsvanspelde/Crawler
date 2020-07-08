using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class FollowPlayer : AIState
{
    public UnityEvent InAttackRange = new UnityEvent();
    Transform player;
    private EnemyStats enemyStats;

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

        while (agent.hasPath)
        {
            SetAgentDestination(agent);
            yield return new WaitForSeconds(Time.deltaTime);
            //TODO: Check if we are in attack range of player
            if (Vector3.Distance(player.transform.position, agent.transform.position) <= enemyStats.EnemyWeapon.Stats.AttackRange.GetValue())
            {
                InAttackRange.Invoke();
                Done = true;
            }
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
