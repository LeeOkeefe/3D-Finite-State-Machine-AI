using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GuardRaycasting))]
[RequireComponent(typeof(GuardPatrolling))]
public class GuardBehaviour : MonoBehaviour
{
    [SerializeField] private GuardState CurrentState;
    [SerializeField] private GuardState PreviousState;

    private Animator Anim => GetComponent<Animator>();
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Conversing = Animator.StringToHash("Conversing");

    private void Awake()
    {
        CurrentState = GuardState.Patrolling;
        PreviousState = GuardState.Idle;
        UpdateChildrenStates();
    }

    private void SetAnimation()
    {
        switch (CurrentState)
        {
            case GuardState.Idle:
                Anim.SetTrigger(Idle);
                break;
            case GuardState.Patrolling:
                Anim.SetFloat(Speed, 0.5f);
                break;
            case GuardState.Attacking:
                break;
            case GuardState.Chasing:
                Anim.SetFloat(Speed, 1f);
                break;
            case GuardState.Conversing:
                Anim.SetTrigger(Conversing);
                break;
            case GuardState.Investigating:
                break;
            case GuardState.Resetting:
                break;
            default:
                Anim.SetFloat(Speed, 0);
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

        UpdateChildrenStates();
        SetAnimation();
    }

    private void UpdateChildrenStates()
    {
        GetComponent<GuardPatrolling>().GuardState = CurrentState;
        GetComponent<GuardRaycasting>().CurrentGuardState = CurrentState;
    }
}
