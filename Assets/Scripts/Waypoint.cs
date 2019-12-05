using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
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

            var waypoints = transform.parent.GetComponentsInChildren<Waypoint>().ToList();
            waypoints.Remove(this);

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
}
