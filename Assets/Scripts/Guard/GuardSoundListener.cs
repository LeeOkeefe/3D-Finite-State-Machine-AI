using UnityEngine;

namespace Guard
{
    [RequireComponent(typeof(GuardBehaviour))]
    public class GuardSoundListener : MonoBehaviour, IBehaviour
    {
        private GuardState m_CurrentState;
        private GuardBehaviour m_Behaviour;

        public void TriggerSound(float volumeScale, Vector3 position)
        {
            if (m_CurrentState == GuardState.Chasing)
                return;

            Debug.Log($"I heard something at volume {volumeScale}!");

            m_Behaviour.HeardAudioSource(position);
        }

        public void Initialize()
        {
            m_Behaviour = GetComponent<GuardBehaviour>();
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;
        }
    }
}
