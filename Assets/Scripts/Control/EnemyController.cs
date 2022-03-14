using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] [Range(0, 100f)] float chaseDistance = 5f;

        GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (IsPlayerInChaseRange())
            {
                Chase();
            }
        }

        bool IsPlayerInChaseRange()
        {
            if (player == null) return false;

            return Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
        }

        void Chase()
        {
            Debug.Log($"{gameObject.name} is chasing player!");
        }
    }
}