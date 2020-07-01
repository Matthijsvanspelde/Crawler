using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    public bool Done = false;

    public UnityEvent transition;

    public virtual void AwakeState()
    {

    }

    public virtual void HandleState()
    {

    }

    public virtual void EndState()
    {

    }

}
