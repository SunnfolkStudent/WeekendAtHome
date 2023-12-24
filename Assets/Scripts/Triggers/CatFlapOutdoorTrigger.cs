using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFlapOutdoorTrigger : MonoBehaviour
{
    // Declare Variables
    
    private String _insideOrOutside;
    
    private bool _switched = false;
    
    [SerializeField] private float moveAmount;
    [SerializeField] private GameObject cat;
    
    void Start()
    {
        // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
        // and hide all of the outdoor objects
        
        _insideOrOutside = "Inside";
        cat = GameObject.FindWithTag("Cat");
    }

    private void Update()
    {
        //Debug.Log(transform.position.y - cat.transform.position.y);
        float diff = transform.position.y - cat.transform.position.y;

        if (diff < 0.1 && diff > -0.1 && !_switched)
        {
            exitOrEnterHouse();
            _switched = true;
        }
        else { _switched = false; }
    }

    private void exitOrEnterHouse()
    {
        if (_insideOrOutside.Equals("Inside"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);
            
            cat.GetComponent<SpriteRenderer>().sortingOrder = -1;
            _insideOrOutside = "Inside";
        }
        else
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);

            cat.GetComponent<SpriteRenderer>().sortingOrder = 50;
            _insideOrOutside = "Inside";
        }
    }
}
