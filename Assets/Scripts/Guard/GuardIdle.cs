
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Guard
{
    public class GuardIdle : MonoBehaviour, IBehaviour
    {
        private GuardBehaviour m_Behaviour;
        private NavMeshAgent m_Agent;
        private GuardState m_CurrentState;

        private float m_Timer;
        private float m_MaxTime;

        private Random m_Random;

        private static readonly int Idle = Animator.StringToHash("Idle");

        private void Update()
        {
            if (m_CurrentState != GuardState.Idle)
                return;

            m_Timer += Time.deltaTime;

            Debug.Log($"Waited {m_Timer} of {m_MaxTime} seconds");

            if (m_Timer >= m_MaxTime)
            {
                m_Agent.isStopped = false;
                m_Behaviour.animator.SetBool(Idle, false);
                m_Behaviour.EndIdle();
            }
        }

        public void Initialize()
        {
            m_Behaviour = GetComponent<GuardBehaviour>();
            m_Agent = GetComponent<NavMeshAgent>();
            m_Random = new Random();
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;
            m_Timer = 0;

            if (m_CurrentState == GuardState.Idle)
            {
                m_MaxTime = m_Random.Next(2, 6);
                m_Behaviour.animator.SetBool(Idle, true);
                m_Agent.isStopped = true;
            }
        }
    }
}
