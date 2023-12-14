using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{

    // Declare variables
    public static String currentFloor;

    public GameObject[] bottomFloor, topFloor;

    private static int num;
    
    void Start()
    {
        // Fetch all of the objects with the bottomFloor and topFloor tags and store them in a list
        // and hide all of the top floor objects
        
        currentFloor = "BottomFloor";
        
        bottomFloor = GameObject.FindGameObjectsWithTag("BottomFloor");
        topFloor = GameObject.FindGameObjectsWithTag("TopFloor");

        num++;
        
        if (num == 2)
            foreach(GameObject topFloorGameObject in topFloor)
                topFloorGameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ran when the player enters either stair trigger
        // Checks which floor the player is currently on and hides the objects on the same floor
        // Then it activates all of the objects on the floor the player is entering
        
        if (currentFloor.Equals("BottomFloor"))
        {
            foreach(GameObject bottomFloorGameObject in bottomFloor)
                if (!bottomFloorGameObject.Equals(this.gameObject))
                    bottomFloorGameObject.SetActive(false);
            
            foreach(GameObject topFloorGameObject in topFloor)
                topFloorGameObject.SetActive(true);
            
            currentFloor = "TopFloor";
            this.gameObject.SetActive(false);
        }
        else
        {
            
            foreach(GameObject topFloorGameObject in topFloor)
                if (!topFloorGameObject.Equals(this.gameObject))
                    topFloorGameObject.SetActive(false);
            
            foreach(GameObject bottomFloorGameObject in bottomFloor)
                bottomFloorGameObject.SetActive(true);
            
            currentFloor = "BottomFloor";
            this.gameObject.SetActive(false);
        }

        return;
    }
}