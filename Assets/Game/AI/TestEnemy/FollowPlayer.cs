using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : AIState
{
    Transform player;

    private void Awake()
    {
        player = Movement.instance.transform;
    }

    public override void AwakeState(NavMeshAgent agent)
    {

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
