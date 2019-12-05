using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Guard
{
    [RequireComponent(typeof(GuardRaycasting))]
    [RequireComponent(typeof(GuardPatrolling))]
    public class GuardBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject m_Player;
        [SerializeField] private GuardState CurrentState;
        [SerializeField] private GuardState PreviousState;

        private Animator m_Anim;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Conversing = Animator.StringToHash("Conversing");

        private void Awake()
        {
            CurrentState = GuardState.Patrolling;
            PreviousState = GuardState.Idle;
            m_Anim = GetComponent<Animator>();
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

        private void SetAnimation()
        {
            switch (CurrentState)
            {
                case GuardState.Idle:
                    m_Anim.SetTrigger(Idle);
                    break;
                case GuardState.Patrolling:
                    m_Anim.SetFloat(Speed, 0.5f);
                    break;
                case GuardState.Attacking:
                    break;
                case GuardState.Chasing:
                    m_Anim.SetFloat(Speed, 1f);
                    break;
                case GuardState.Conversing:
                    m_Anim.SetTrigger(Conversing);
                    break;
                case GuardState.Investigating:
                    break;
                case GuardState.Resetting:
                    break;
                default:
                    m_Anim.SetFloat(Speed, 0);
                    break;
            }
        }

        public void LostPlayer()
        {
            if (IsInState(GuardState.Chasing))
            {
                GetComponent<GuardInvestigating>().SetInvestigationLocation(m_Player.transform.position);

                TransitionState(GuardState.Investigating);
            }
        }

        public void SeenPlayer()
        {
            if (IsInState(GuardState.Idle,
                GuardState.Patrolling,
                GuardState.Resetting))
            {
                GetComponent<GuardInvestigating>().SetInvestigationLocation(m_Player.transform.position);
                TransitionState(GuardState.Investigating);
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
            var behaviours = GetComponents<IBehaviour>();

            foreach (var behaviour in behaviours)
            {
                behaviour.UpdatedState(CurrentState);
            }
        }
    }
}
