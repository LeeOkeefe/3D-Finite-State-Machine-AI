using UnityEngine;

namespace Assets.Scripts
{
    public interface IBehaviour
    {
        void Initialize();
        void UpdatedState(GuardState newState);
    }
}
