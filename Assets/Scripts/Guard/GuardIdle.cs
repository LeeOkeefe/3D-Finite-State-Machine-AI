using UnityEngine;

namespace Guard
{
    public class GuardIdle : MonoBehaviour, IBehaviour
    {
        private GuardBehaviour m_Behaviour;
        private GuardState m_CurrentState;

        private float m_Timer;
        private float m_MaxTime;

        private System.Random m_Random;

        private void Update()
        {
            if (m_CurrentState != GuardState.Idle)
                return;

            m_Timer += Time.deltaTime;

            if (m_Timer >= m_MaxTime)
            {
                m_Behaviour.EndIdle();
            }
        }

        public void Initialize()
        {
            m_Behaviour = GetComponent<GuardBehaviour>();
            m_Random = new System.Random();
        }

        public void UpdateState(GuardState newState)
        {
            m_Timer = 0;
            m_MaxTime = m_Random.Next(2, 5);
            m_CurrentState = newState;
        }
    }
}
