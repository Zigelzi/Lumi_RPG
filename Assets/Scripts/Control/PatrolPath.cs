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
                Vector3 currentWaypointPosition = GetWaypointPosition(waypointIndex);
                int nextWaypointIndex = GetNextIndex(waypointIndex);
                Vector3 nextWaypoint = GetWaypointPosition(nextWaypointIndex);

                DrawWaypointSphere(currentWaypointPosition);
                DrawWaypointLine(currentWaypointPosition, nextWaypoint);
            }
        }

        public Vector3 GetWaypointPosition(int waypointIndex)
        {
            return transform.GetChild(waypointIndex).position;
        }

        public int GetNextIndex(int waypointIndex)
        {
            if (waypointIndex + 1 == transform.childCount)
            {
                return 0;
            }
            return waypointIndex + 1;
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

