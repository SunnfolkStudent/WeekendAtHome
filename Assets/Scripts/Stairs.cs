using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{

    private String currentFloor;

    private GameObject bottomFloor, topFloor;
    
    // Start is called before the first frame update
    void Start()
    {
        currentFloor = "BottomFloor";
        
        bottomFloor = GameObject.FindGameObjectWithTag("BottomFloor");
        topFloor = GameObject.FindGameObjectWithTag("TopFloor");
        
        topFloor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentFloor.Equals("BottomFloor"))
        {
            bottomFloor.SetActive(false);
            topFloor.SetActive(true);
            
            currentFloor = "TopFloor";
        }
        else
        {
            topFloor.SetActive(false);
            bottomFloor.SetActive(true);
            
            currentFloor = "BottomFloor";
        }

        return;
    }
}
