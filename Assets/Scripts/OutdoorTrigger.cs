using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutdoorTrigger : MonoBehaviour
{
    // Declare Variables
    [SerializeField] private GameObject bottomFloor;
    
    private String insideOrOutside;
    private GameObject[] outdoorToDespawn, outdoor;

    [SerializeField] private float transparencyValue, moveAmount;

    private float playerLightIntensity;
    
    void Start()
    {
        // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
        // and hide all of the outdoor objects
        
        insideOrOutside = "Inside";
        
        outdoorToDespawn = GameObject.FindGameObjectsWithTag("Outdoor to Despawn");
        outdoor = GameObject.FindGameObjectsWithTag("Outdoors");

        playerLightIntensity = GameObject.FindWithTag("Player").GetComponentInChildren<Light2D>().intensity;
        
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
            bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);
            
            foreach (Transform child in bottomFloor.transform)
                child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);
            
            foreach(GameObject outdoorToDespawnObjects in outdoorToDespawn)
                outdoorToDespawnObjects.SetActive(false);
            
            foreach(GameObject outdoorObjects in outdoor)
                outdoorObjects.SetActive(true);

            transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);
            
            GameObject.FindWithTag("Player").GetComponentInChildren<Light2D>().intensity = 0;
            insideOrOutside = "Outside";
        }
        else
        {
            bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
            foreach (Transform child in bottomFloor.transform)
                child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
            foreach(GameObject outdoorToDespawnObjects in outdoorToDespawn)
                outdoorToDespawnObjects.SetActive(true);
            
            foreach(GameObject outdoorObjects in outdoor)
                outdoorObjects.SetActive(false);
            
            transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);
            
            GameObject.FindWithTag("Player").GetComponentInChildren<Light2D>().intensity = playerLightIntensity;
            insideOrOutside = "Inside";
        }

        return;
    }
}
