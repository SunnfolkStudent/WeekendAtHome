using UnityEngine;

namespace Triggers
{
    public class BathroomTrigger : MonoBehaviour
    {
        // Declare Variables
        [SerializeField] private GameObject insideBathroomToDespawn;
        [SerializeField] private float moveAmount;
    
        void Start()
        {
            insideBathroomToDespawn = GameObject.FindWithTag("Inside Bathroom to Despawn");
        }

        // Sets the bathroom to inactive while you are outside the bathroom and on the top floor
        void Update()
        {
            if (DataTransfer.InsideBathroom)
            {
                insideBathroomToDespawn.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Ran when the player enters the bathroom trigger
            // Checks if the player is currently inside or outside
            // If inside (going out), SetActive outsideBathroom objects, and vice versa if outside (going in).
        
            if (DataTransfer.InsideBathroom)
            {
                insideBathroomToDespawn.SetActive(false);
                transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);
            }
            else
            {
                insideBathroomToDespawn.SetActive(true);
                transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);
            }
            DataTransfer.EnterOrExitBathroom();
        }
    }
}
