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

        public bool m_Opened;

        private void Start()
        {
            m_Audio = GetComponent<AudioSource>();
        }

        public virtual void Interact()
        {
            if (m_Opened)
                return;

            StartCoroutine(OpenDoor());
            m_Audio.PlayOneShot(m_Audio.clip);
            m_Opened = true;
        }

        /// <summary>
        /// Rotate the door open using <see cref="Quaternion.Slerp"/>
        /// </summary>
        protected IEnumerator OpenDoor()
        {
            if (m_CurrentTime > 0 || m_Opened)
                yield break;

            var endRotation = transform.rotation * Quaternion.Euler(angle);

            while (m_CurrentTime < m_OpenTime)
            {
                m_CurrentTime += Time.deltaTime;
                var percent = m_CurrentTime / m_OpenTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, percent);
                yield return null;
            }
        }
    }
}
