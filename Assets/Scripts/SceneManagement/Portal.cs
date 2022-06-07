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
        [SerializeField][Range(0,5f)] float sceneFadeOutDuration = 1f;
        [SerializeField] [Range(0, 5f)] float sceneFadeWaitDuration = .5f;
        [SerializeField] [Range(0, 5f)] float sceneFadeInDuration = .5f;

        enum PortalIdentifier
        {
            A, B
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
            SavingWrapper saving = FindObjectOfType<SavingWrapper>();

            DontDestroyOnLoad(gameObject);
            yield return canvasFader.FadeOut(sceneFadeOutDuration);

            saving.Save();

            yield return SceneManager.LoadSceneAsync(destinationSceneIndex);

            saving.Load();

            Portal otherPortal = GetOtherPortal(identifier);
            UpdatePlayerLocation(otherPortal);

            saving.Save();

            yield return new WaitForSeconds(sceneFadeWaitDuration);
            yield return canvasFader.FadeIn(sceneFadeInDuration);

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

