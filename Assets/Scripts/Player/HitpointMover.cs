using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointMover : MonoBehaviour
{
    #region SingleTon
    public static HitpointMover instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion
    
    [SerializeField] private Transform pointToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = pointToFollow.transform.rotation;
    }
}
