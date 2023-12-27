using UnityEngine;
using UnityEngine.Rendering;

namespace Triggers
{
    public class CatFlapOutdoorTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject cat;
        [SerializeField] private SortingGroup catSortingGroup;
        [SerializeField] private bool catOutside;
    
        void Start()
        {
            // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
            // and hide all of the outdoor objects
            cat = GameObject.FindWithTag("Cat");
            catSortingGroup = cat.GetComponent<SortingGroup>();
        
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
            if (catSortingGroup == null)
                return;
            catSortingGroup.sortingOrder = 50;
            Debug.Log("CatIsInside");
        }
    }
}
