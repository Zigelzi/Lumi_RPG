using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float gizmoRadius = .25f;
        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            DrawWaypointGizmos();
        }

        void DrawWaypointGizmos()
        {
            for (int waypointIndex = 0; waypointIndex < transform.childCount; waypointIndex++)
            {
                Transform currentWaypoint = transform.GetChild(waypointIndex);
                DrawWaypointSphere(currentWaypoint.position);

                if (waypointIndex < transform.childCount - 1)
                {
                    Transform nextWaypoint = transform.GetChild(waypointIndex + 1);
                    DrawWaypointLine(currentWaypoint.position, nextWaypoint.position);
                }
                if (waypointIndex == transform.childCount - 1)
                {
                    Transform firstWaypoint = transform.GetChild(0);
                    DrawWaypointLine(currentWaypoint.position, firstWaypoint.position);
                }
            }
        }

        void DrawWaypointSphere(Vector3 waypointPosition)
        {
            Gizmos.DrawSphere(waypointPosition, gizmoRadius);
        }

        void DrawWaypointLine(Vector3 currentWaypointPosition, Vector3 nextWayPointPosition)
        {
            Gizmos.DrawLine(currentWaypointPosition, nextWayPointPosition);
        }
    }
}

