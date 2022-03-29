using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    
    public class Portal : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] int destinationSceneIndex = 0;
        [SerializeField] Transform spawnPoint;
        void Awake()
        {
              
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(TransitionToScene());
            }
        }

        IEnumerator TransitionToScene()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(destinationSceneIndex);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayerLocation(otherPortal);

            Destroy(gameObject);
        }

        Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();

            foreach (Portal portal in portals)
            {
                if (portal == this)
                {
                    continue;
                }

                return portal;
            }

            return null;
        }

        void UpdatePlayerLocation(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<NavMeshAgent> ().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}

