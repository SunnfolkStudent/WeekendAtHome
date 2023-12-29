using System;
using System.Collections;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

namespace Triggers
{
    // Make sure this script runs before StairsTrigger.cs, in Project Settings, in Script Execution Order.
    public class FloorManager : MonoBehaviour
    {
        [Header("Bottom Floor:")]
        public GameObject bottomFloor;
        public GameObject stairsBottomFloor;
        public GameObject toesInteraction;
        
        [Header("Top Floor:")]
        public GameObject topFloor;
        public GameObject stairsTopFloor;
        public GameObject bathroomTrigger;

        // Awake is called before any Start functions, across scripts.
        private void Awake()
        {
            StartCoroutine(EnableAllFloorsAndItems());
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(DisableAllFloorsExceptCurrent());
        }
        
        private IEnumerator EnableAllFloorsAndItems()
        {
            bottomFloor.SetActive(true);
            stairsBottomFloor.SetActive(true);
            toesInteraction.SetActive(true);

            topFloor.SetActive(true);
            stairsTopFloor.SetActive(true);
            bathroomTrigger.SetActive(true);
            
            yield break;
        }

        private IEnumerator DisableAllFloorsExceptCurrent()
        {
            if (!DataTransfer.OnTopFloor)
            {
                topFloor.SetActive(false);
                stairsTopFloor.SetActive(false);
                bathroomTrigger.SetActive(false);
            }
            else if (DataTransfer.OnTopFloor)
            {
                bottomFloor.SetActive(false);
                stairsBottomFloor.SetActive(false);
                toesInteraction.SetActive(false);
            }
            yield break;
        }
    }
}
