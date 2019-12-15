using UnityEngine;
using UnityEngine.AI;

namespace Guard
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

        private static readonly int Investigate = Animator.StringToHash("Investigate");
        private static readonly int Speed = Animator.StringToHash("Speed");

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
                m_Behaviour.animator.SetBool(Investigate, true);
                m_Timer += Time.deltaTime;

                if (m_Timer >= m_InvestigationTime)
                {
                    m_Behaviour.animator.SetFloat(Speed, 0.1f);
                    m_Behaviour.animator.SetBool(Investigate, false);
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
