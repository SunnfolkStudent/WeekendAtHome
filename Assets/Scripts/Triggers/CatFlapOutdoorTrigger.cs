using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Triggers
{
    public class CatFlapOutdoorTrigger : MonoBehaviour
    {
        // Declare Variables
    
        // private String _insideOrOutside;

        [SerializeField] private float moveAmount;
        [SerializeField] private GameObject cat;
        [SerializeField] private CircleCollider2D catCollider;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BoxCollider2D catFlapTriggerBox;
        [SerializeField] private SortingGroup catSortingGroup;
        [SerializeField] private bool catOutside;
        
        // private bool _switched = false;
    
        void Start()
        {
            // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
            // and hide all of the outdoor objects
            cat = GameObject.FindWithTag("Cat");
            catCollider = cat.GetComponent<CircleCollider2D>();
            catSortingGroup = cat.GetComponent<SortingGroup>();
            rb = GetComponent<Rigidbody2D>();
            catFlapTriggerBox = GetComponent<BoxCollider2D>();
        
            if (catOutside)
            {
                Debug.Log("CatIsOutside");
                catSortingGroup.sortingOrder = 2;
            }
            else if (!catOutside)
            {
                Debug.Log("CatIsInside");
                catSortingGroup.sortingOrder = 50;
            }
        }

        /* private void Update()
         {
            /* /Debug.Log(transform.position.y - cat.transform.position.y);
            var diff = transform.position.y - cat.transform.position.y;

            // TODO: Notes to self:
            // When the cat enters the triggerBox2D, cat ends up outside/inside.
            // While inside triggerBox2D box, initiate while condition, and temporarily disable code until the triggerBox2D is no longer active.
            //
            // Maybe have 2 triggerBox2D boxes, to ensure that the cat truly does go outside, and doesn't change course within the cat-flap.

            if (diff < 0.1 && diff > -0.1 && !triggerBox2D.isTrigger)
            {
                triggerBox2D.isTrigger = true;
                ExitOrEnterHouse();
            }

            while (triggerBox2D.isTrigger)
            {
                if (diff > 0.1 && diff < -0.1 && triggerBox2D.isTrigger)
                {

                }

                if (!triggerBox2D.isTrigger)
                {
                    break;
                }
            }
        } */

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;
            if (!collision.CompareTag("Cat")) return;
            catOutside = true;
            catSortingGroup.sortingOrder = 2;
            Debug.Log("CatIsOutside");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            catOutside = false;
            catSortingGroup.sortingOrder = 50;
            Debug.Log("CatIsInside");
        }
    }
}
