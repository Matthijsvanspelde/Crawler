using UnityEngine;
using UnityEngine.AI;

public class AIState : MonoBehaviour
{
    [HideInInspector]
    public bool Done = false;
    [HideInInspector]
    public StatLine Stats;

    private void Awake()
    {
        Stats = GetComponentInParent<StatLine>();
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
