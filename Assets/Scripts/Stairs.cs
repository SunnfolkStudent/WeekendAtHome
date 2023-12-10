using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{

    private String currentFloor;

    private GameObject[] bottomFloor, topFloor;
    
    // Start is called before the first frame update
    void Start()
    {
        currentFloor = "BottomFloor";
        
        bottomFloor = GameObject.FindGameObjectsWithTag("BottomFloor");
        topFloor = GameObject.FindGameObjectsWithTag("TopFloor");
        
        foreach(GameObject topFloorGameObject in topFloor)
            topFloorGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentFloor.Equals("BottomFloor"))
        {
            foreach(GameObject bottomFloorGameObject in bottomFloor)
                bottomFloorGameObject.SetActive(false);
            
            foreach(GameObject topFloorGameObject in topFloor)
                topFloorGameObject.SetActive(true);
            
            currentFloor = "TopFloor";
        }
        else
        {
            foreach(GameObject bottomFloorGameObject in bottomFloor)
                bottomFloorGameObject.SetActive(true);
            
            foreach(GameObject topFloorGameObject in topFloor)
                topFloorGameObject.SetActive(false);
            
            currentFloor = "BottomFloor";
        }

        return;
    }
}
