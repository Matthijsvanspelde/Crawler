using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class State : MonoBehaviour
{
    [HideInInspector]
    public bool Done = false;

    public EnemyStats Stats;

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
