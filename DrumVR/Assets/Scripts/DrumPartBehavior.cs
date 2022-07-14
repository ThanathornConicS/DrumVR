using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class DrumPartBehavior : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] clips;

    [Header("Set Hit Angle")]
    public float hitAngle = 90f;

    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Collider"))
        {
            ObjectVelocity ov = collider.gameObject.GetComponent<ObjectVelocity>();
            Vector3 rigidbodyVel = rb.velocity;
            float velMagnitude = rigidbodyVel.magnitude;
            float angle = Vector3.Angle(Vector3.down, rigidbodyVel);
            
            if(angle < hitAngle){
                VibrationManager.Instance.TriggerVibration(m_audioSource.clip, collider.gameObject);
                m_audioSource.pitch = Random.Range(0.95f, 1.05f);

                if(velMagnitude <= 0.3f)
                {
                    m_audioSource.clip = clips[0];
                }
                else if(velMagnitude > 0.3f && velMagnitude <= 0.5f)
                {
                    m_audioSource.clip = clips[1];
                }
                else if(velMagnitude > 0.5f && velMagnitude <= 0.7f)
                {
                    m_audioSource.clip = clips[2];
                }
                else if(velMagnitude > 0.7f)
                {
                    m_audioSource.clip = clips[3];
                }

                m_audioSource.Play();
            }
        }
    }
}