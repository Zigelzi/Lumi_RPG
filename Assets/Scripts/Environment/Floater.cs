using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Environment
{
    public class Floater : MonoBehaviour
    {
        [SerializeField] bool isFloating = false;
        [SerializeField][Range(0, 10f)] float floatingSpeed = 2f;
        [SerializeField][Range(0, 1f)] float maxDistance = .5f;
        [SerializeField] float destinationRadius = .1f;

        bool isMovingUp = true;
        Vector3 startingPosition;
        Vector3 startingDestination;
        Vector3 currentDestination;

        // Start is called before the first frame update
        void Start()
        {
            startingPosition = transform.position;
            startingDestination = new Vector3(transform.position.x,
                transform.position.y + maxDistance,
                transform.position.z);
            currentDestination = startingDestination;
        }

        // Update is called once per frame
        void Update()
        {
            Float();
        }

        void Float()
        {
            
            float distanceFromDestination = Vector3.Distance(transform.position, currentDestination);
            float step = floatingSpeed * Time.deltaTime;

            if (isFloating && distanceFromDestination >= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentDestination, step);
            }

            if (Vector3.Distance(transform.position, startingPosition) < destinationRadius)
            {
                currentDestination = startingDestination;
            }
            else if (Vector3.Distance(transform.position, startingDestination) < destinationRadius)
            {
                currentDestination = startingPosition;
            }
        }
    }
}
