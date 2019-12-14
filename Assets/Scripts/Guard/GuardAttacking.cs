using Player;
using UnityEngine;

namespace Guard
{
    public class GuardAttacking : MonoBehaviour, IBehaviour
    {
        [SerializeField] private GameObject player;

        private PlayerHealth m_PlayerHealth;
        private GuardState m_CurrentState;

        private void Update()
        {
            if (m_CurrentState != GuardState.Attacking)
                return;

            if (Vector3.Distance(transform.position, player.transform.position) < 1)
            {
                m_PlayerHealth.Kill();
            }
        }

        public void Initialize()
        {
            m_PlayerHealth = player.GetComponent<PlayerHealth>();
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;
        }
    }
}
