using UnityEngine;

namespace Assets.Scripts.Guard
{
    [RequireComponent(typeof(GuardBehaviour))]
    public class GuardSoundListener : MonoBehaviour, IBehaviour
    {
        private GuardState m_CurrentState;
        private GuardBehaviour m_Behaviour;

        public void TriggerSound(float volumeScale, Vector3 position)
        {
            Debug.Log($"I heard something at volume {volumeScale}!");

            if (m_CurrentState == GuardState.Chasing)
                return;

            m_Behaviour.HeardAudioSource(position);
        }

        public void Initialize()
        {
            m_Behaviour = GetComponent<GuardBehaviour>();
            Debug.Log("GUARD SOUND LISTENER INITIALIZED");
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;
        }
    }
}
