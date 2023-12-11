using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorTrigger : MonoBehaviour
{
    // Declare Variables
    [SerializeField] private GameObject bottomFloor;
    
    private String insideOrOutside;
    
    private GameObject[] outdoorToDespawn, outdoor;
    

    void Start()
    {
        // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
        // and hide all of the outdoor objects
        
        insideOrOutside = "Inside";
        
        outdoorToDespawn = GameObject.FindGameObjectsWithTag("Outdoor to Despawn");
        outdoor = GameObject.FindGameObjectsWithTag("Outdoors");
        
        foreach(GameObject outdoorObjects in outdoor)
            outdoorObjects.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Ran when the player enters the outdoor trigger
        // Checks if the player is currently inside or outside
        // Then it activates all of the objects corresponding to where the player is + hiding objects that need to be hidden
        
        if (insideOrOutside.Equals("Inside"))
        {
            bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.38f);
            
            foreach(GameObject outdoorToDespawnObjects in outdoorToDespawn)
                outdoorToDespawnObjects.SetActive(false);
            
            foreach(GameObject outdoorObjects in outdoor)
                outdoorObjects.SetActive(true);
            
            insideOrOutside = "Outside";
        }
        else
        {
            bottomFloor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
            foreach(GameObject outdoorToDespawnObjects in outdoorToDespawn)
                outdoorToDespawnObjects.SetActive(true);
            
            foreach(GameObject outdoorObjects in outdoor)
                outdoorObjects.SetActive(false);
            
            insideOrOutside = "Inside";
        }

        return;
    }
}
