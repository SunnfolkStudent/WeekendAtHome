using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{

    private String currentFloor;

    public GameObject[] bottomFloor, topFloor;
    

    void Start()
    {
        currentFloor = "BottomFloor";
        
        bottomFloor = GameObject.FindGameObjectsWithTag("BottomFloor");
        topFloor = GameObject.FindGameObjectsWithTag("TopFloor");
        
        foreach(GameObject topFloorGameObject in topFloor)
                topFloorGameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentFloor.Equals("BottomFloor"))
        {
            foreach(GameObject bottomFloorGameObject in bottomFloor)
                if (!bottomFloorGameObject.Equals(this.gameObject))
                    bottomFloorGameObject.SetActive(false);
            
            foreach(GameObject topFloorGameObject in topFloor)
                topFloorGameObject.SetActive(true);
            
            currentFloor = "TopFloor";
            //this.gameObject.SetActive(false);
        }
        else
        {
            foreach(GameObject bottomFloorGameObject in bottomFloor)
                bottomFloorGameObject.SetActive(true);
            
            foreach(GameObject topFloorGameObject in topFloor)
                if (!topFloorGameObject.Equals(this.gameObject))
                    topFloorGameObject.SetActive(false);
            
            currentFloor = "BottomFloor";
            this.gameObject.SetActive(true);
        }

        return;
    }
}
