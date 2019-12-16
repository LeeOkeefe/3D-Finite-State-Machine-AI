using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Guard
{
    [RequireComponent(typeof(GuardBehaviour))]
    public class GuardPatrolling : MonoBehaviour, IBehaviour
    {
        private GuardBehaviour m_Behaviour;
        private NavMeshAgent m_Agent;
        private GuardState m_CurrentState;

        [SerializeField] private List<Waypoint> m_Waypoints;

        [SerializeField] private Waypoint m_CurrentWaypoint;
        [SerializeField] private Waypoint m_PreviousWaypoint;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Update()
        {
            if (m_CurrentState != GuardState.Patrolling)
                return;

            if (!m_Agent.hasPath)
            {
                m_Agent.SetDestination(m_CurrentWaypoint.transform.position);
            }

            if (m_Agent.hasPath && m_Agent.remainingDistance <= 0.5f)
            {
                var random = new Random();
                var roll = random.Next(0, 5);

                if (roll == 0)
                {
                    m_Behaviour.SetIdle();
                    return;
                }

                CalculateNextWaypoint();
            }
        }

        /// <summary>
        /// Set destination to new randomized linked waypoint
        /// </summary>
        private void CalculateNextWaypoint()
        {
            var nextWaypoints = m_CurrentWaypoint.LinkedWaypoints.ToList();

            if (m_PreviousWaypoint != null)
            {
                nextWaypoints.Remove(m_PreviousWaypoint);
            }

            var random = new Random();
            var next = random.Next(0, nextWaypoints.Count);

            m_PreviousWaypoint = m_CurrentWaypoint;
            m_CurrentWaypoint = nextWaypoints[next];

            m_Agent.SetDestination(m_CurrentWaypoint.transform.position);
        }

        /// <summary>
        /// Returns the waypoint we are closest to
        /// </summary>
        private Waypoint GetClosestWaypoint()
        {
            var start = Mathf.Infinity;
            Waypoint closestWaypoint = null;

            foreach (var waypoint in m_Waypoints)
            {
                var distance = Vector3.Distance(transform.position, waypoint.transform.position);

                if (distance < start)
                {
                    start = distance;
                    closestWaypoint = waypoint;
                }
            }

            return closestWaypoint;
        }

        public void Initialize()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Behaviour = GetComponent<GuardBehaviour>();
        }

        public void UpdateState(GuardState newState)
        {
            m_CurrentState = newState;

            if (newState == GuardState.Patrolling)
            {
                m_Behaviour.animator.SetFloat(Speed, 0.1f);
                m_Agent.speed = 1.75f;

                if (m_CurrentWaypoint == null)
                {
                    m_CurrentWaypoint = GetClosestWaypoint();
                }
                else
                {
                    CalculateNextWaypoint();
                }
            }
        }
    }
}
