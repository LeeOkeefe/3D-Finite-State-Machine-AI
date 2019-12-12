using System.Collections;
using UnityEngine;

namespace Objects
{
    internal class Door : MonoBehaviour, IInteractive
    {
        [SerializeField] private Vector3 angle;
        private AudioSource m_Audio;

        private float m_OpenTime = 2;
        private float m_CurrentTime;

        private void Start()
        {
            m_Audio = GetComponent<AudioSource>();
        }

        public virtual void Interact()
        {
            StartCoroutine(OpenDoor());
            m_Audio.PlayOneShot(m_Audio.clip);
        }

        /// <summary>
        /// Rotate the door open using <see cref="Quaternion.Slerp"/>
        /// </summary>
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
