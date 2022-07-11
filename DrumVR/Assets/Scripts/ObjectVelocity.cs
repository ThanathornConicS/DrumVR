using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVelocity : MonoBehaviour
{
    Vector3 NewPosition;
    Vector3 PrevPosition;

    public Vector3 Velocity {get; private set;} = Vector3.zero;

    void Start()
    {
        NewPosition = transform.position;
        PrevPosition = transform.position;
    }

    void FixedUpdate()
    {
        NewPosition = transform.position; 
        Velocity = (NewPosition - PrevPosition) / Time.fixedDeltaTime; 
        PrevPosition = NewPosition;  
    }
}