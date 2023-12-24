using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BathroomTrigger : MonoBehaviour
{
    // Declare Variables
    [SerializeField] private GameObject bathroomObject;
    
    private String insideOrOutsideBathroom;
    private GameObject[] insideBathroomToDespawn, bathroom;

    [SerializeField] private float moveAmount;

    
    void Start()
    {
        insideOrOutsideBathroom = "Inside";
        
        insideBathroomToDespawn = GameObject.FindGameObjectsWithTag("Inside Bathroom to Despawn");
        bathroom = GameObject.FindGameObjectsWithTag("Top Floor Inside Bathroom");
        
    }

    // Sets the bathroom to inactive while you are outside the bathroom and on the top floor
    void Update()
    {
        if (StairsTrigger.currentFloor.Equals("TopFloor") && insideOrOutsideBathroom.Equals("Inside"))
        {
            
            bathroomObject.SetActive(false);
            
            foreach (GameObject toDespawn in bathroom)
                toDespawn.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ran when the player enters the bathroom trigger
        // Checks if the player is currently inside or outside
        // Then it activates all of the objects corresponding to where the player is + hiding objects that need to be hidden
        // Then moves itself down

        // Does opposite when player reenters

        if (insideOrOutsideBathroom.Equals("Inside"))
        {
            bathroomObject.SetActive(false);

            foreach(GameObject inBathroomToDespawnObjects in insideBathroomToDespawn)
                inBathroomToDespawnObjects.SetActive(false);

            foreach(GameObject bathroomObjects in bathroom)
                bathroomObjects.SetActive(true);

            transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);
            insideOrOutsideBathroom = "Inside";
        }
        else
        {
            bathroomObject.SetActive(true);

            foreach(GameObject inBathroomToDespawnObjects in insideBathroomToDespawn)
                inBathroomToDespawnObjects.SetActive(true);

            foreach(GameObject bathroomObjects in bathroom)
                bathroomObjects.SetActive(false);

            transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);
            insideOrOutsideBathroom = "Inside";
        }

        return;

    }
}
