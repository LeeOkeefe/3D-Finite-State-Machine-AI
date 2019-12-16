using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;

namespace Guard
{
    public class GuardRaycasting : MonoBehaviour, IBehaviour
    {
        private GuardBehaviour Behaviour;
        public GameObject Player;

        private float m_SeenTimer;
        private float m_LostTimer;

        [SerializeField] private SightState m_SightState;

        private GuardState m_GuardState;

        private void Update()
        {
            SearchForPlayer();
        }

        private void SearchForPlayer()
        {
            var targetDir = Player.transform.position - transform.position;
            var angle = Vector3.Angle(targetDir, transform.forward);

            if (m_SightState == SightState.NotSeen && m_GuardState == GuardState.Chasing)
            {
                m_LostTimer += Time.deltaTime;

                if (m_LostTimer >= 5f)
                {
                    Behaviour.LostPlayer();
                }
            }

            if (angle > 100f)
            {
                TransitionState(SightState.NotSeen);
                return;
            }

            Physics.Linecast(transform.position, Player.transform.position, out var hitInfo);

            if (hitInfo.transform != Player.transform)
            {
                TransitionState(SightState.NotSeen);
                return;
            }

            m_SeenTimer += Time.deltaTime;
            if (m_SightState == SightState.NotSeen)
            {
                var distance = Vector3.Distance(transform.position, Player.transform.position);
                TransitionState(distance <= 15f ? SightState.LongSeen : SightState.ShortSeen);
            }
            else if (m_SightState == SightState.ShortSeen)
            {
                if (m_SeenTimer > 1.5f)
                {
                    TransitionState(SightState.LongSeen);
                }
            }
        }

        private void TransitionState(SightState state)
        {
            if (m_SightState == state)
                return;

            m_SightState = state;

            switch (state)
            {
                case SightState.NotSeen:
                    m_SeenTimer = 0f;
                    break;
                case SightState.ShortSeen:
                    m_LostTimer = 0f;
                    Behaviour.SeenPlayer();
                    break;
                case SightState.LongSeen:
                    m_LostTimer = 0f;
                    Behaviour.LongSight();
                    break;
            }
        }

        private enum SightState
        {
            NotSeen,
            ShortSeen,
            LongSeen
        }

        public void Initialize()
        {
            Behaviour = GetComponent<GuardBehaviour>();
        }

        public void UpdateState(GuardState newState)
        {
            m_GuardState = newState;
        }
    }
}
