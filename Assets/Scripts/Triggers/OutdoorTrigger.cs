using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Triggers
{
    public class OutdoorTrigger : MonoBehaviour
    {
        // Declare Variables
        [SerializeField] private GameObject bottomFloor;
    
        private String insideOrOutside;
        private GameObject[] outdoorToDespawn, outdoor;

        [SerializeField] private float transparencyValue, moveAmount, playerOutdoorLightValue;

        private float playerLightIntensity;

        private GameObject player;

        [SerializeField] private FootstepManager footstepManager;
    
        void Start()
        {
            // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
            // and hide all of the outdoor objects
        
            insideOrOutside = "Inside";
            footstepManager.isOutside = false;
        
            outdoorToDespawn = GameObject.FindGameObjectsWithTag("Outdoor to Despawn");
            outdoor = GameObject.FindGameObjectsWithTag("Outdoors");
        
            player = GameObject.FindWithTag("Player");
        
            playerLightIntensity = player.GetComponentInChildren<Light2D>().intensity;
        
            foreach(GameObject outdoorObjects in outdoor)
                outdoorObjects.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        
            // Ran when the player enters the outdoor trigger
            // Checks if the player is currently inside or outside
            // Then it activates all of the objects corresponding to where the player is + hiding objects that need to be hidden
            // Sets the light2d child of player to an intensity of 0 and then moves down
        
            // Does opposite when player reenters
        
            if (insideOrOutside.Equals("Inside"))
            {
                footstepManager.isOutside = true;
            
                bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);
            
                foreach (Transform child in bottomFloor.transform)
                    child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);
            
                foreach(GameObject outdoorToDespawnObjects in outdoorToDespawn)
                    outdoorToDespawnObjects.SetActive(false);
            
                foreach(GameObject outdoorObjects in outdoor)
                    outdoorObjects.SetActive(true);

                transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);
            
                player.GetComponentInChildren<Light2D>().intensity = playerOutdoorLightValue;
                player.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
                insideOrOutside = "Outside";
            }
            else
            {
                footstepManager.isOutside = false;
            
                bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
                foreach (Transform child in bottomFloor.transform)
                    child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
                foreach(GameObject outdoorToDespawnObjects in outdoorToDespawn)
                    outdoorToDespawnObjects.SetActive(true);
            
                foreach(GameObject outdoorObjects in outdoor)
                    outdoorObjects.SetActive(false);
            
                transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);
            
                player.GetComponentInChildren<Light2D>().intensity = playerLightIntensity;
                player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 50;
                insideOrOutside = "Inside";
            }

            return;
        }
    }
}
