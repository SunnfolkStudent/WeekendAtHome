using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorTrigger : MonoBehaviour
{

    [SerializeField] private GameObject bottomFloor;
    
    private String insideOrOutside;
    
    private GameObject[] outdoorToDespawn, outdoor;

    public static int num;

    void Start()
    {
        insideOrOutside = "Inside";
        
        outdoorToDespawn = GameObject.FindGameObjectsWithTag("Outdoor to Despawn");
        outdoor = GameObject.FindGameObjectsWithTag("Outdoors");

        num++;
        Debug.Log(num);
        
        if (num == 3)
            foreach(GameObject outdoorObjects in outdoor)
                outdoorObjects.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
