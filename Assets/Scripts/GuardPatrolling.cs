using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class GuardPatrolling : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public GuardState GuardState;

    [SerializeField] private List<Waypoint> m_Waypoints;

    [SerializeField] private Waypoint m_CurrentWaypoint;
    [SerializeField] private Waypoint m_PreviousWaypoint;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_CurrentWaypoint = GetClosestWaypoint();
    }

    private void Update()
    {
        if (GuardState != GuardState.Patrolling)
            return;

        if (!m_Agent.hasPath)
        {
            m_Agent.SetDestination(m_CurrentWaypoint.transform.position);
        }

        if (m_Agent.remainingDistance == 0f)
        {
            // Get next possible waypoints
            var nextWaypoints = m_CurrentWaypoint.LinkedWaypoints.ToList();
            print(m_CurrentWaypoint.LinkedWaypoints.Count);
            print(nextWaypoints.Count);
            // If we just came from a node, remove it from the list of possible waypoints
            if (m_PreviousWaypoint != null)
            {
                nextWaypoints.Remove(m_PreviousWaypoint);
            }
            // Pick random waypoint from potential points
            var random = new Random();
            var next = random.Next(0, nextWaypoints.Count);

            print("Next node is at index " + next + " and our nodes size is " + nextWaypoints.Count);
            // Our current node is now our previous, and the node
            // we are about to go to is now our current node
            m_PreviousWaypoint = m_CurrentWaypoint;
            m_CurrentWaypoint = nextWaypoints[next];

            m_Agent.SetDestination(m_CurrentWaypoint.transform.position);
        }
    }

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
}
