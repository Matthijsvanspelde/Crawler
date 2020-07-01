using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateMachine : MonoBehaviour
{
    [Tooltip("Make sure default state is the first one to be in the list")]
    [SerializeField] public List<State> lstStates = new List<State>();

    [SerializeField] public State defaultState = null;

    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public State currentState = null;

    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null && currentState.Done)
        {
            currentState.EndState();
            currentState.Done = false;
            NextState();
            PlayCurrentState();
        }
    }

    private void NextState()
    {
        int index = lstStates.IndexOf(currentState) + 1;

        if (index <= lstStates.Count)
            currentState = lstStates[index];
        else
            currentState = lstStates[0];

    }

    private void PlayCurrentState()
    {
        currentState?.AwakeState();
        currentState?.HandleState();
    }
}
