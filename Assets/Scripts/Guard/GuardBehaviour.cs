using System.Linq;
using UnityEngine;

namespace Guard
{
    [RequireComponent(typeof(GuardRaycasting))]
    [RequireComponent(typeof(GuardPatrolling))]
    public class GuardBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject m_Player;
        [SerializeField] private GuardState CurrentState;
        [SerializeField] private GuardState PreviousState;

        public Animator animator;

        private void Awake()
        {
            CurrentState = GuardState.Patrolling;
            PreviousState = GuardState.Idle;
            animator = GetComponent<Animator>();
            InitializeBehaviourStates();
            UpdateChildrenStates();
        }

        private void InitializeBehaviourStates()
        {
            var behaviours = GetComponents<IBehaviour>();

            foreach (var behaviour in behaviours)
            {
                behaviour.Initialize();
            }
        }

        public void LostPlayer()
        {
            if (IsInState(GuardState.Chasing))
            {
                SetInvestigation(m_Player.transform.position);
            }
        }

        public void SeenPlayer()
        {
            if (IsInState(GuardState.Idle,
                GuardState.Patrolling))
            {
                SetInvestigation(m_Player.transform.position);
            }
        }

        public void AttackPlayer()
        {
            TransitionState(GuardState.Attacking);
        }

        public void SetIdle()
        {
            if (IsInState(GuardState.Patrolling))
            {
                TransitionState(GuardState.Idle);
            }
        }

        public void EndIdle()
        {
            TransitionState(GuardState.Patrolling);
        }

        public void HeardAudioSource(Vector3 position)
        {
            if (!IsInState(GuardState.Chasing,
                           GuardState.Attacking))
            {
                SetInvestigation(position);
            }
        }

        public void EndInvestigation()
        {
            TransitionState(GuardState.Patrolling);
        }

        public void LongSight()
        {
            if (IsInState(GuardState.Idle,
                          GuardState.Investigating,
                          GuardState.Patrolling))
            {
                TransitionState(GuardState.Chasing);
            }
        }

        private void SetInvestigation(Vector3 position)
        {
            GetComponent<GuardInvestigating>().SetInvestigationLocation(position);
            TransitionState(GuardState.Investigating);
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
            Debug.Log(newState);
        }

        private void UpdateChildrenStates()
        {
            var behaviours = GetComponents<IBehaviour>();

            foreach (var behaviour in behaviours)
            {
                behaviour.UpdateState(CurrentState);
            }
        }
    }
}
