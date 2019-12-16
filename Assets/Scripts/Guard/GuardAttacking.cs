using Player;
using UnityEngine;

namespace Guard
{
    public class GuardAttacking : MonoBehaviour, IBehaviour
    {
        [SerializeField] private GameObject player;

        private PlayerHealth m_PlayerHealth;
        private GuardBehaviour m_Behaviour;

        private GuardState m_CurrentState;

        private float m_Timer;

        private static readonly int Attack = Animator.StringToHash("Attack");

        private void Update()
        {
            if (m_CurrentState != GuardState.Attacking)
                return;

            m_Behaviour.Animator.SetBool(Attack, true);

            m_Timer += Time.deltaTime;

            if (m_Timer > 1.25f)
            {
                m_PlayerHealth.Kill();
            }
        }

        public void Initialize()
        {
            m_PlayerHealth = player.GetComponent<PlayerHealth>();
            m_Behaviour = GetComponent<GuardBehaviour>();
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;
        }
    }
}
