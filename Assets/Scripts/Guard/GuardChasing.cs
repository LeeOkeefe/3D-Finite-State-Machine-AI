using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Guard
{
    [RequireComponent(typeof(GuardBehaviour))]
    public class GuardChasing : MonoBehaviour, IBehaviour
    {
        [SerializeField] private GameObject m_Player;

        private NavMeshAgent m_Agent;
        private GuardState m_CurrentState;

        private void Update()
        {
            if (m_CurrentState != GuardState.Chasing)
                return;

            m_Agent.SetDestination(m_Player.transform.position);
        }

        public void Initialize()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }

        public void UpdatedState(GuardState newState)
        {
            m_CurrentState = newState;
        }
    }
}
