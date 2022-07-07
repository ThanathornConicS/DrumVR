using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VibrationManager : MonoBehaviour
{

    public static VibrationManager Instance;

    [SerializeField]
    ActionBasedController leftController;
    [SerializeField]
    ActionBasedController rightController;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        leftController = GameObject.Find("LeftHand Controller").GetComponent<ActionBasedController>();
        rightController = GameObject.Find("RightHand Controller").GetComponent<ActionBasedController>();
    }
    
    public void TriggerVibration(AudioClip clip, GameObject stick)
    {
        if(stick.name == "Left Stick")
            {
                Debug.Log("Left is Triggered");
                leftController.SendHapticImpulse(0.7f, 2f);
            }
            else
            {
                Debug.Log("Right is Triggered");
                rightController.SendHapticImpulse(0.7f, 2f);
            }
    }
}
