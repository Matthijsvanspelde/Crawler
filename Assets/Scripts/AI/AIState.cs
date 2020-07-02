using UnityEngine;
using UnityEngine.AI;

public class AIState : MonoBehaviour
{
    [HideInInspector]
    public bool Done = false;
    [HideInInspector]
    public StatHolder Stats;

    private void Awake()
    {
        Stats = GetComponentInParent<StatHolder>();
    }

    public virtual void AwakeState(NavMeshAgent agent)
    {

    }

    public virtual void HandleState(NavMeshAgent agent)
    {

    }

    public virtual void EndState(NavMeshAgent agent)
    {

    }
}
