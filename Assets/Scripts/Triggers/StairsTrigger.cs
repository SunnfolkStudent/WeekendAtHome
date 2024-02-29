using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Triggers
{
    // Make sure this script runs after FloorManager.cs in Project Settings, in Script Execution Order.
    public class StairsTrigger : MonoBehaviour
    {
        // Declare variables
        public GameObject cat;
        public GameObject[] bottomFloorArray;
        public GameObject[] topFloorArray;
        public GameObject pauseScreen;
    
        private void Awake()
        {
            // Apply tags to all activated gameObjects from FloorManager.
            
            bottomFloorArray = GameObject.FindGameObjectsWithTag("BottomFloor");
            topFloorArray = GameObject.FindGameObjectsWithTag("TopFloor");
            cat = GameObject.FindWithTag("Cat");
            pauseScreen = GameObject.FindWithTag("PauseScene");

            /* if (DataTransfer.OnTopFloor)
            {
                foreach(GameObject bottomFloorGameObject in bottomFloorArray)
                    if (!bottomFloorGameObject.Equals(this.gameObject))
                        bottomFloorGameObject.SetActive(false);

                foreach(GameObject topFloorGameObject in topFloorArray)
                    topFloorGameObject.SetActive(true);
            }
            else
            {
                foreach(GameObject topFloorGameObject in topFloorArray)
                    if (!topFloorGameObject.Equals(this.gameObject))
                        topFloorGameObject.SetActive(false);

                foreach(GameObject bottomFloorGameObject in bottomFloorArray)
                    bottomFloorGameObject.SetActive(true);
            } */
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!DataTransfer.onTopFloor)
            {
                foreach(GameObject bottomFloorGameObject in bottomFloorArray)
                    if (!bottomFloorGameObject.Equals(this.gameObject))
                        bottomFloorGameObject.SetActive(false);
            
                foreach(GameObject topFloorGameObject in topFloorArray)
                    topFloorGameObject.SetActive(true);
                
                // gameObject.layer uses only integers, but we can turn a layer name into a layer integer using LayerMask.NameToLayer()
                // The code below assigns the gameObject "cat" the layer with the name "Cat".
                
                int layerCat = LayerMask.NameToLayer("Cat");
                cat.layer = layerCat;
                // Debug.Log("Current layer (cat): Cat");
            }
            else if (DataTransfer.onTopFloor)
            {
                foreach(GameObject topFloorGameObject in topFloorArray)
                    if (!topFloorGameObject.Equals(this.gameObject))
                        topFloorGameObject.SetActive(false);
            
                foreach(GameObject bottomFloorGameObject in bottomFloorArray)
                    bottomFloorGameObject.SetActive(true);
                
                int layerDefault = LayerMask.NameToLayer("Default");
                cat.layer = layerDefault;
                // Debug.Log("Current layer (cat): Default");
            }
            DataTransfer.SwitchFloors();
        }
    }
}
