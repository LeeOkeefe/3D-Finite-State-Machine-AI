using Assets.Scripts.Guard;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IBehaviour
    {
        void Initialize();
        void UpdateState(GuardState newState);
    }
}
