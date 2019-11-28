using System.Collections;
using UnityEngine;

namespace Objects
{
    internal sealed class Drawer : MonoBehaviour, IInteractive
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private float lerpTime = 1f;

        private float m_CurrentLerpTime;

        public void Interact()
        {
            StartCoroutine(OpenDrawer());
        }

        private IEnumerator OpenDrawer()
        {
            var endPosition = transform.position + position;

            while (m_CurrentLerpTime < lerpTime)
            {
                m_CurrentLerpTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * lerpTime);
                yield return null;
            }

            Destroy(this);
        }
    }
}
