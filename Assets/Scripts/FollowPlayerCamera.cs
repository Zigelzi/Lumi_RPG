using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = player.transform.position;
    }
}
