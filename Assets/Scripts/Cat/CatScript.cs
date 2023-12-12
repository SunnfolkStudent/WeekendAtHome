using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    public AIPath aiPath;
    private bool onTheMove = true;
    
    private void FixedUpdate()
    {
        if (onTheMove)
        {
            if (aiPath.desiredVelocity.x >= 0.1)
            {
                Debug.Log("Moving Right");
            }
        
            if (aiPath.desiredVelocity.x <= -0.1)
            {
                Debug.Log("Moving Left");
            }
            if (aiPath.desiredVelocity.y >= 0.1)
            {
                Debug.Log("Moving Up");
            }
        
            if (aiPath.desiredVelocity.y <= -0.1)
            {
                Debug.Log("Moving Down");
            }

            if (aiPath.reachedDestination)
            {
                onTheMove = false;
                Debug.Log("Reached Destination");
            }
            
        }
    }
}