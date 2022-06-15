using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class Billboard : MonoBehaviour
    {
        Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
    }
}

