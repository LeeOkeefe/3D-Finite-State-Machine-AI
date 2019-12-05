using System.Collections;
using Objects;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    internal class Door : MonoBehaviour, IInteractive
    {
        [SerializeField] private Vector3 angle;

        private float m_OpenTime = 2;
        private float m_CurrentTime;

        public virtual void Interact()
        {
            StartCoroutine(OpenDoor());
        }

        protected IEnumerator OpenDoor()
        {
            if (m_CurrentTime > 0)
                yield break;

            var endRotation = transform.rotation * Quaternion.Euler(angle);

            while (m_CurrentTime < m_OpenTime)
            {
                m_CurrentTime += Time.deltaTime;
                var percent = m_CurrentTime / m_OpenTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, percent);
                yield return null;
            }

            m_CurrentTime = 0;
            Destroy(this);
        }
    }
}
