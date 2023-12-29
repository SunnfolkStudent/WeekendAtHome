using System;
using UnityEngine;

namespace Triggers
{
    // Make sure this script runs after FloorManager.cs in Project Settings, in Script Execution Order.
    public class StairsTrigger : MonoBehaviour
    { 
        // Declare variables
        public static String CurrentFloor;

        public GameObject[] bottomFloorArray;
        public GameObject[] topFloorArray;
    
        private void Awake()
        {
            // Fetch all of the objects with the bottomFloorArray and topFloorArray tags and store them in a list
            // and hide all of the top floor objects
            if (!DataTransfer.OnTopFloor) 
                CurrentFloor = "BottomFloor";
            else
                CurrentFloor = "TopFloor";
        
            bottomFloorArray = GameObject.FindGameObjectsWithTag("BottomFloor");
            topFloorArray = GameObject.FindGameObjectsWithTag("TopFloor");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!DataTransfer.OnTopFloor)
            {
                foreach(GameObject bottomFloorGameObject in bottomFloorArray)
                    if (!bottomFloorGameObject.Equals(this.gameObject))
                        bottomFloorGameObject.SetActive(false);
            
                foreach(GameObject topFloorGameObject in topFloorArray)
                    topFloorGameObject.SetActive(true);
                // this.gameObject.SetActive(false);
            }
            else if (DataTransfer.OnTopFloor)
            {
                foreach(GameObject topFloorGameObject in topFloorArray)
                    if (!topFloorGameObject.Equals(this.gameObject))
                        topFloorGameObject.SetActive(false);
            
                foreach(GameObject bottomFloorGameObject in bottomFloorArray)
                    bottomFloorGameObject.SetActive(true);
                // this.gameObject.SetActive(false);
            }
            DataTransfer.SwitchFloors();
        }
    }
}
