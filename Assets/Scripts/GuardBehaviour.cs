using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GuardRaycasting))]
public class GuardBehaviour : MonoBehaviour
{
    [SerializeField] private GuardState CurrentState;
    [SerializeField] private GuardState PreviousState;

    private void Awake()
    {
        CurrentState = GuardState.Patrolling;
        PreviousState = GuardState.Idle;
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case GuardState.Patrolling:
                // GuardMovement.Patrol();
                break;
            case GuardState.Resetting:
                // GuardMovement.Reset();
                break;
            case GuardState.Chasing:
                // GuardMovement.Chase();
                break;
        }
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
    }

    private enum GuardState
    {
        Attacking,
        Chasing,
        Conversing,
        Idle,
        Investigating,
        Patrolling,
        Resetting
    }
}
