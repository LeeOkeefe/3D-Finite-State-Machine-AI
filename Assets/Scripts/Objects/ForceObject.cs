using UnityEngine;

namespace Objects
{
    internal sealed class ForceObject : MonoBehaviour
    {
        [SerializeField]
        [Range(10, 500)]
        private float force = 150;

        private Rigidbody Rb => GetComponent<Rigidbody>();

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
            AddForce(other.GetComponent<Collider>(), Rb);
        }
    }
}