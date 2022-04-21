using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Update()
    {
        FlyTowards();
    }

    void FlyTowards()
    {
        Vector3 aimLocation = GetAimLocation();
        transform.LookAt(aimLocation);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    Vector3 GetAimLocation()
    {
        CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
        Vector3 targetCenter = Vector3.up * targetCollider.height / 2;
        return target.position + targetCenter;
    }
}
