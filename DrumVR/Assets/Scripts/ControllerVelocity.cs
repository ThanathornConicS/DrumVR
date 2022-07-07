using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ControllerVelocity : MonoBehaviour
{
    public InputActionProperty velocityProperty;
    public Vector3 Velocity {get; private set;} = Vector3.zero;

    public TMP_Text velText;

    [SerializeField]
    private float velocityMagnitude = 0.0f;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        Velocity = velocityProperty.action.ReadValue<Vector3>();
        velocityMagnitude = Velocity.magnitude;

        //Debug.Log("Magnitude: " + Velocity);
        velText.text = "Velocity" + Velocity + " with magnitude: " + velocityMagnitude;
    }
}
