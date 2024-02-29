using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBools : MonoBehaviour
{
    public bool PlayerCanMove = true; // PlayerCanMove should be true from the start.
    public bool OnTopFloor = false; // Change this in Editor if you're changing starting floors.
    
    // Update is called once per frame
    void Update()
    {
        if (PlayerCanMove)
        {
            DataTransfer.playerCanMove = true;
        }
        else
        {
            DataTransfer.playerCanMove = false;
        }

        if (OnTopFloor)
        {
            DataTransfer.onTopFloor = true;
        }
        else
        {
            DataTransfer.onTopFloor = false;
        }
    }
}
