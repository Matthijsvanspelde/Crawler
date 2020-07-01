using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Tooltip("Make sure default state is the first one to be in the list")]
    [SerializeField] private List<State> lstStates = new List<State>();

    [SerializeField] private State defaultState = null;

    private State currentState = null;

    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState.Done)
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
