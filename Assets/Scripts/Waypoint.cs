using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private float radius = 0.3f;
    [SerializeField]
    public List<Waypoint> LinkedWaypoints;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var linkedWaypoint in LinkedWaypoints)
        {
            if (linkedWaypoint == null)
                continue;

            Gizmos.DrawLine(transform.position, linkedWaypoint.transform.position);
        }
    }


    public void CalculateLinkedNodes()
    {
        LinkedWaypoints.Clear();

        // Gather nodes under parent
        var waypoints = transform.parent.GetComponentsInChildren<Waypoint>().ToList();
        waypoints.Remove(this);

        // Cast a ray from ourself to all other nodes under the parent
        foreach (var waypoint in waypoints)
        {
            Physics.Linecast(transform.position, waypoint.transform.position, out var hit);

            if (hit.transform == waypoint.transform)
            {
                LinkedWaypoints.Add(waypoint);
            }
        }
    }
}
