using System;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(AudioSource))]
    internal sealed class ForceObject : MonoBehaviour
    {
        [SerializeField]
        [Range(10, 500)]
        private float force = 150;

        private Rigidbody m_Rb; 
        private AudioSource m_Audio;

        private void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
            m_Audio = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Calculates the opposite direction of the given collider,
        /// and adds force to this RigidBody in that direction
        /// </summary>
        private void AddForce(Collider col, Rigidbody rb)
        {
            var direction = (transform.position - col.gameObject.transform.position).normalized;

            rb.AddForce(direction * force);
        }

        private void OnTriggerEnter(Collider other)
        {
            AddForce(other.GetComponent<Collider>(), m_Rb);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.relativeVelocity.magnitude >= 3.5)
            {
                m_Audio.PlayOneShot(m_Audio.clip);
            }
        }
    }
}