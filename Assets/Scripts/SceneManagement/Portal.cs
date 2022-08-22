using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour, IRaycastable
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

        public CursorType GetCursorType()
        {
            return CursorType.Interactable;
        }

        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            player.TryStartMoveAction(hit.point);
            return true;
        }

        IEnumerator TransitionToScene()
        {
            CanvasFader canvasFader = FindObjectOfType<CanvasFader>();
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            SavingWrapper saving = FindObjectOfType<SavingWrapper>();

            player.enabled = false;

            DontDestroyOnLoad(gameObject);
            yield return canvasFader.FadeToOpaque(sceneFadeOutDuration);

            saving.Save();

            yield return SceneManager.LoadSceneAsync(destinationSceneIndex);

            // Disable player again because new player is spawned when level is loaded
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.enabled = false;

            saving.Load();

            Portal otherPortal = GetOtherPortal(identifier);
            UpdatePlayerLocation(otherPortal);

            saving.Save();

            yield return new WaitForSeconds(sceneFadeWaitDuration);
            canvasFader.FadeToTransparent(sceneFadeInDuration);

            player.enabled = true;
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

