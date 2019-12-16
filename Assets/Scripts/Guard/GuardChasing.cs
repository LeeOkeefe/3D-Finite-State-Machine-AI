using UnityEngine;
using UnityEngine.AI;

namespace Guard
{
    [RequireComponent(typeof(GuardBehaviour))]
    public class GuardChasing : MonoBehaviour, IBehaviour
    {
        [SerializeField] private GameObject m_Player;

        private GuardBehaviour m_Behaviour;
        private NavMeshAgent m_Agent;
        private GuardState m_CurrentState;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Update()
        {
            if (m_CurrentState != GuardState.Chasing)
                return;

            m_Agent.SetDestination(m_Player.transform.position);

            if (Vector3.Distance(transform.position, m_Player.transform.position) < 1)
            {
                m_Behaviour.AttackPlayer();
            }
        }

        public void Initialize()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Behaviour = GetComponent<GuardBehaviour>();
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;

            if (m_CurrentState == GuardState.Chasing)
            {
                m_Behaviour.Animator.SetFloat(Speed, 1);
                m_Agent.speed = 5f;
            }
        }
    }
}
