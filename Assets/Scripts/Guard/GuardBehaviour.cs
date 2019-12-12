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

        private Animator m_Anim;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Conversing = Animator.StringToHash("Conversing");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Investigate = Animator.StringToHash("Investigate");

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
                    m_Anim.SetTrigger(Attack);
                    break;
                case GuardState.Chasing:
                    m_Anim.SetFloat(Speed, 1f);
                    break;
                case GuardState.Conversing:
                    m_Anim.SetTrigger(Conversing);
                    break;
                case GuardState.Investigating:
                    m_Anim.SetTrigger(Investigate);
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

        public void HeardAudioSource(Vector3 position)
        {
            if (!IsInState(GuardState.Chasing))
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
            SetAnimation();
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
