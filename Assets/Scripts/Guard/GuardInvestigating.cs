using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Guard
{
    [RequireComponent(typeof(GuardBehaviour))]
    public class GuardInvestigating : MonoBehaviour, IBehaviour
    {
        private NavMeshAgent m_Agent;
        private GuardBehaviour m_Behaviour;

        public Vector3 InvestigationLocation;
        private GuardState m_CurrentState;

        private float m_InvestigationTime = 5f;
        private float m_Timer;

        private void Update()
        {
            if (m_CurrentState != GuardState.Investigating)
                return;

            if (m_Agent.destination != InvestigationLocation)
            {
                m_Agent.SetDestination(InvestigationLocation);
            }

            if (m_Agent.remainingDistance < 0.5f)
            {
                m_Timer += Time.deltaTime;

                if (m_Timer >= m_InvestigationTime)
                {
                    m_Behaviour.EndInvestigation();
                }
            }
        }

        public void SetInvestigationLocation(Vector3 location)
        {
            InvestigationLocation = location;
        }

        public void Initialize()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Behaviour = GetComponent<GuardBehaviour>();
        }

        public void UpdateState(GuardState newState)
        {
            m_Timer = 0f;
            m_CurrentState = newState;
        }
    }
}
