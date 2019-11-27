using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GuardRaycasting))]
[RequireComponent(typeof(GuardPatrolling))]
public class GuardBehaviour : MonoBehaviour
{
    [SerializeField] private GuardState CurrentState;
    [SerializeField] private GuardState PreviousState;

    private void Awake()
    {
        CurrentState = GuardState.Patrolling;
        PreviousState = GuardState.Idle;
        UpdateChildrenStates();
    }

    public void LostSight()
    {
        if (IsInState(GuardState.Chasing))
        {
            TransitionState(GuardState.Resetting);
        }
    }

    public void SeenPlayer()
    {
        if (IsInState(GuardState.Idle,
                      GuardState.Patrolling,
                      GuardState.Resetting))
        {
            TransitionState(GuardState.Investigating);
        }
    }

    public void LongSight()
    {
        if (IsInState(GuardState.Idle,
                      GuardState.Investigating,
                      GuardState.Resetting,
                      GuardState.Patrolling))
        {
            TransitionState(GuardState.Chasing);
        }
    }

    private bool IsInState(params GuardState[] states)
    {
        return states.Any(state => CurrentState == state);
    }

    private void TransitionState(GuardState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        PreviousState = CurrentState;
        CurrentState = newState;

        UpdateChildrenStates();
    }

    private void UpdateChildrenStates()
    {
        GetComponent<GuardPatrolling>().GuardState = CurrentState;
        GetComponent<GuardRaycasting>().CurrentGuardState = CurrentState;
    }
}
