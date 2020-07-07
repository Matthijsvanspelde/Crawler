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
    
    [SerializeField] private Camera cam;

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
        transform.rotation = cam.transform.rotation;
    }
}
