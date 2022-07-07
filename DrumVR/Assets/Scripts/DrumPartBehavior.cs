using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class DrumPartBehavior : MonoBehaviour
{
    public float hitAngle = 90f;

    AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Stick"))
        {
            ControllerVelocity controllerVelocity = collider.gameObject.transform.parent.gameObject.GetComponent<ControllerVelocity>();
            float angle = Vector3.Angle(Vector3.down, controllerVelocity.Velocity);
            
            if(angle < hitAngle){
                VibrationManager.Instance.TriggerVibration(m_audioSource.clip, collider.gameObject);
                m_audioSource.pitch = Random.Range(0.95f, 1.05f);
                m_audioSource.Play();
            }
        }
    }
}