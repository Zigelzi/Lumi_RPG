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
        [SerializeField] PortalIdentifier identifier;
        [SerializeField][Range(0,5f)] float sceneFadeDuration = 1f;

        enum PortalIdentifier
        {
            A, B
        }

        void Update()
        {
            if (!Application.isPlaying)
            {
                //gameObject.name = $"Portal_{identifier}";
            }
                     
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
            CanvasFader canvasFader = FindObjectOfType<CanvasFader>();

            DontDestroyOnLoad(gameObject);
            yield return canvasFader.FadeOut(sceneFadeDuration);

            yield return SceneManager.LoadSceneAsync(destinationSceneIndex);
            Portal otherPortal = GetOtherPortal(identifier);
            UpdatePlayerLocation(otherPortal);

            yield return new WaitForSeconds(1f);
            yield return canvasFader.FadeIn(sceneFadeDuration);
            

            Destroy(gameObject);
        }

        Portal GetOtherPortal(PortalIdentifier destinationPortalIdentifier)
        {
            Portal[] portals = FindObjectsOfType<Portal>();

            foreach (Portal portal in portals)
            {
                
                if (portal != this && portal.identifier == destinationPortalIdentifier)
                {
                    return portal;
                }
            }

            return null;
        }

        void UpdatePlayerLocation(Portal otherPortal)
        {
            if (otherPortal == null)
            {
                Debug.LogError("No other portal was found");
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<NavMeshAgent> ().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}

