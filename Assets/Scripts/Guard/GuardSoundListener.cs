using UnityEngine;

namespace Assets.Scripts.Guard
{
    public class GuardSoundListener : MonoBehaviour, IBehaviour
    {
        public void TriggerSound(float volumeScale)
        {
            Debug.Log($"I heard something at volume {volumeScale}!");
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void UpdatedState(GuardState newState)
        {
            throw new System.NotImplementedException();
        }
    }
}
