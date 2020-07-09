using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    private List<AIState> lstStates = new List<AIState>();

    [SerializeField] public AIState defaultState = null;

    public NavMeshAgent agent;
    //[HideInInspector]
    public AIState currentState = null;

    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
        SetList();
        PlayCurrentState();
    }

    public void GoToSpecificState(AIState stateToTransitionTo)
    {
        CloseCurrentState();
        currentState = stateToTransitionTo;
        PlayCurrentState();
    }

    private void SetList()
    {
        foreach (AIState state in GetComponentsInChildren<AIState>())
        {
            lstStates.Add(state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null && currentState.Done)
        {
            CloseCurrentState();
            NextState();
            PlayCurrentState();
        }
    }

    private void CloseCurrentState()
    {
        currentState.Done = false;
        currentState.EndState(agent);
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
        currentState?.AwakeState(agent);
        currentState?.HandleState(agent);
    }
}
