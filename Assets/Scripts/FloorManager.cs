using System.Collections;
using UnityEngine;

// Make sure this script runs before StairsTrigger.cs, in Project Settings, in Script Execution Order.
    public class FloorManager : MonoBehaviour
    {
        [Header("Bottom Floor:")]
        public GameObject bottomFloor;
        public GameObject stairsBottomFloor;
        public BoxCollider2D toesTriggerBox;
        
        [Header("Top Floor:")]
        public GameObject topFloor;
        public GameObject stairsTopFloor;
        public GameObject bathroomTrigger;

        [Header("Cat:")] 
        public GameObject cat;
        public SpriteRenderer catSprite;

        [Header("PauseScreen")] 
        public GameObject pauseScreen;

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
            toesTriggerBox.enabled = true;

            topFloor.SetActive(true);
            stairsTopFloor.SetActive(true);
            bathroomTrigger.SetActive(true);

            catSprite.enabled = true;
            
            pauseScreen.SetActive(true);
            
            yield break;
        }

        private IEnumerator DisableAllFloorsExceptCurrent()
        {
            if (!DataTransfer.onTopFloor)
            {
                topFloor.SetActive(false);
                stairsTopFloor.SetActive(false);
                bathroomTrigger.SetActive(false);
                toesTriggerBox.enabled = true;
                catSprite.enabled = true;
                pauseScreen.SetActive(false);
                
                int layerDefault = LayerMask.NameToLayer("Default");
                cat.layer = layerDefault;
                Debug.Log("Current cat layer: Default");
            }
            else if (DataTransfer.onTopFloor)
            {
                AstarPath.active.ScanAsync();
                
                bottomFloor.SetActive(false);
                stairsBottomFloor.SetActive(false);
                toesTriggerBox.enabled = false;
                catSprite.enabled = false;
                pauseScreen.SetActive(false);
                
                int layerCat = LayerMask.NameToLayer("Cat");
                cat.layer = layerCat;
                Debug.Log("Current cat layer: Cat");
            }
            yield break;
        }
    }
