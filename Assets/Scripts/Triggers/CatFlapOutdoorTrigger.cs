using System;
using Cat;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Rendering;

namespace Triggers
{
    public class CatFlapOutdoorTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject cat;
        [SerializeField] private SortingGroup catSortingGroup;
    
        private void Awake()
        {
            // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
            // and hide all of the outdoor objects
            cat = GameObject.FindWithTag("Cat");
            if (cat != null)
            {
                catSortingGroup = cat.GetComponent<SortingGroup>();
            }
            
            /* if (DataTransfer.CatOutside)
            {
                Debug.Log("CatIsOutside");
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderOutside;
            }
            else if (!DataTransfer.CatOutside)
            {
                Debug.Log("CatIsInside");
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderInside;
            } */
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;
            if (!collision.CompareTag("Cat")) return;
            
            // catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderOutside;
            
            DataTransfer.CatOutside = true;
            
            Debug.Log("CatIsOutside");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == null) return;
            if (!collision.CompareTag("Cat")) return;
            
            DataTransfer.CatOutside = false;
            Debug.Log("CatIsInside");
            // The line is to prevent errors during Unity Load/Unload in Editor with missing SortingGroup.
            if (catSortingGroup == null)
                return;
            // catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderInside;
        }
    }
}
