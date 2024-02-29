using System;
using ItemScripts;
using UnityEngine;

namespace Cat
{
    public class CatInteractionScript : MonoBehaviour
    {
        [SerializeField] private CatScript catScript;
        private bool _triggerActive;

        private void Start()
        {
            catScript = GameObject.FindWithTag("Cat").GetComponent<CatScript>();
        }

        private void Update()
        {
            if (_triggerActive && ItemObjectScript.inItemCutscene)
            {
                catScript.catMaxSpeed = 0f;
            }
            else
            {
                catScript.catMaxSpeed = 2f;
            }
        }

        // TODO: Investigate why these triggers are not working...
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            print("Player has entered Cat Trigger");
            if (!other.CompareTag("Player")) return;
            _triggerActive = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            print("Player has left Cat Trigger");
            if (!other.CompareTag("Player")) return;
            _triggerActive = false;
        }
    }
}
