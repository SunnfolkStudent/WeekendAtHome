using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Triggers
{
    // TODO: Fix black semi-transparent overlay over house when going outside!
    // TODO: Make sure the VFX Weather shows better onto the snow
    // TODO: Fix the cat going in and out of the house! (Currently ends up on a layer underneath the house after player has gone outside).
    public class OutdoorTrigger : MonoBehaviour
    {
        // Declare Variables
        [SerializeField] private GameObject bottomFloor;
        private GameObject[] _outdoorToDespawn, _outdoor;
        
        [SerializeField] private GameObject player;
        [SerializeField] private SortingGroup playerSortingGroup;
        
        [SerializeField] private float transparencyValue, moveAmount, playerOutdoorLightValue;
        private float _playerLightIntensity;
        
        void Start()
        {
            // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
            // and hide all of the outdoor objects.
            _outdoorToDespawn = GameObject.FindGameObjectsWithTag("Outdoor to Despawn");
            _outdoor = GameObject.FindGameObjectsWithTag("Outdoors");

            player = GameObject.FindWithTag("Player");

            _playerLightIntensity = player.GetComponentInChildren<Light2D>().intensity;
            playerSortingGroup = player.GetComponent<SortingGroup>();

            if (DataTransfer.PlayerInside)
            {
                Debug.Log("PlayerIsInside");
            }
            if (!DataTransfer.PlayerInside)
            {
                Debug.Log("PlayerIsOutside");
                foreach (GameObject outdoorObjects in _outdoor)
                    outdoorObjects.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Ran when the player enters the outdoor trigger
            // Checks if the player is currently inside or outside
            // Then it activates all of the objects corresponding to where the player is + hiding objects that need to be hidden
            // Sets the light2d child of player to an intensity of 0 and then moves down
            // Does opposite when player reenter.
            
            
            if (!DataTransfer.PlayerInside)
            {
                Debug.Log("Player Triggers Outdoors");

                bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);

                /*foreach (Transform child in bottomFloor.transform)
                    child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue); */

                foreach (GameObject outdoorToDespawnObjects in _outdoorToDespawn)
                    outdoorToDespawnObjects.SetActive(false); 

                /*foreach (GameObject outdoorObjects in _outdoor)
                    outdoorObjects.SetActive(true);*/

                transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);

                player.GetComponentInChildren<Light2D>().intensity = playerOutdoorLightValue;
                playerSortingGroup.sortingOrder = DataTransfer.PlayerSortingOrder;
            }
            else if (DataTransfer.PlayerInside)
            {
                Debug.Log("Player Triggers Inside");
            
                bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
                /* foreach (Transform child in bottomFloor.transform) 
                    child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); */
            
                foreach(GameObject outdoorToDespawnObjects in _outdoorToDespawn)
                    outdoorToDespawnObjects.SetActive(true);
            
                /*foreach(GameObject outdoorObjects in _outdoor)
                    outdoorObjects.SetActive(false);*/
            
                transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);
            
                player.GetComponentInChildren<Light2D>().intensity = _playerLightIntensity;
                playerSortingGroup.sortingOrder = DataTransfer.PlayerSortingOrder;
            }
            DataTransfer.PlayerInsideOrOutside();
        }
    }
}