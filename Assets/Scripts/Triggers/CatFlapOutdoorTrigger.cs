using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CatFlapOutdoorTrigger : MonoBehaviour
{
    // Declare Variables
    
    private String insideOrOutside;
    
    private bool switched = false;
    
    [SerializeField] private float moveAmount;
    [SerializeField] private GameObject cat;
    
    void Start()
    {
        // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
        // and hide all of the outdoor objects
        
        insideOrOutside = "Inside";
        cat = GameObject.FindWithTag("Cat");
    }

    private void Update()
    {
        //Debug.Log(transform.position.y - cat.transform.position.y);
        float diff = transform.position.y - cat.transform.position.y;

        if (diff < 0.1 && diff > -0.1 && !switched)
        {
            exitOrEnterHouse();
            switched = true;
        }
        else { switched = false; }
    }

    private void exitOrEnterHouse()
    {
        
        if (insideOrOutside.Equals("Inside"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);
            
            cat.GetComponent<SpriteRenderer>().sortingOrder = -1;
            insideOrOutside = "Outside";
        }
        else
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);

            cat.GetComponent<SpriteRenderer>().sortingOrder = 50;
            insideOrOutside = "Inside";
        }

        return;
    }
}
