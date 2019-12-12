using Guard;
using UnityEngine;

public interface IBehaviour
{
    void Initialize();
    void UpdateState(GuardState newState);
}