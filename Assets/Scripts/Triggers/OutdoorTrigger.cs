using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Triggers
{
    // TODO: Fix black semi-transparent overlay over house when going outside!
    // TODO: Make sure the VFX Weather shows better onto the snow
    public class OutdoorTrigger : MonoBehaviour
    {
        // Declare Variables
        [SerializeField] private GameObject[] _bottomFloor;
        [SerializeField] private GameObject[] kitchenWithDoorAndLamp;

        [SerializeField] private GameObject backgroundBottomFloor;
        
        [SerializeField] private GameObject player;
        [SerializeField] private SortingGroup playerSortingGroup;
        [SerializeField] private SortingGroup vfxSortingGroup;
        
        [SerializeField] private float transparencyValue, moveAmount, playerOutdoorLightValue;
        private float _playerLightIntensity;
        
        void Start()
        {
            // Fetch all of the objects with the Outdoor and OutdoorToDespawn tags and store them in a list
            // and hide all of the outdoor objects.
            
            _bottomFloor = GameObject.FindGameObjectsWithTag("BottomFloor");
            
            player = GameObject.FindWithTag("Player");

            _playerLightIntensity = player.GetComponentInChildren<Light2D>().intensity;
            
            playerSortingGroup = player.GetComponent<SortingGroup>();
            playerSortingGroup.sortingOrder = DataTransfer.PlayerSortingOrder;
            
            vfxSortingGroup = GameObject.FindWithTag("VFX").GetComponent<SortingGroup>();
            vfxSortingGroup.sortingOrder = DataTransfer.VFXSortingOrder;

            if (DataTransfer.PlayerInside)
            {
                foreach (GameObject bottomFloorGameObject in _bottomFloor)
                {
                    bottomFloorGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }

                foreach (GameObject kitchenAndMoreGameObject in kitchenWithDoorAndLamp)
                {
                    kitchenAndMoreGameObject.GetComponent<SpriteRenderer>().sortingOrder = 45;
                }
                
                
                Debug.Log("OutdoorTrigger - Player Is Inside");
            }
            if (!DataTransfer.PlayerInside)
            {
                foreach (GameObject bottomFloorGameObject in _bottomFloor)
                {
                    bottomFloorGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);
                }
                
                foreach (GameObject kitchenAndMoreGameObject in kitchenWithDoorAndLamp)
                {
                    kitchenAndMoreGameObject.GetComponent<SpriteRenderer>().sortingOrder = 55;
                }
                
                Debug.Log("OutdoorTrigger - Player Is Outside");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;
            if (!collision.CompareTag("Player")) return;
            
            // Ran when the player enters the outdoor trigger
            // Checks if the player is currently inside or outside
            // Then it activates all of the objects corresponding to where the player is + hiding objects that need to be hidden
            // Sets the light2d child of player to an intensity of 0 and then moves down
            // Does opposite when player reenter.
            
            if (DataTransfer.PlayerInside)
            {
                Debug.Log("Player Goes Outdoors");

                foreach (GameObject bottomFloorGameObject in _bottomFloor)
                {
                    bottomFloorGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparencyValue);
                }
                
                foreach (GameObject kitchenAndMoreGameObject in kitchenWithDoorAndLamp)
                {
                    kitchenAndMoreGameObject.GetComponent<SpriteRenderer>().sortingOrder = 55;
                }

                transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount);

                player.GetComponentInChildren<Light2D>().intensity = playerOutdoorLightValue;
            }
            else if (!DataTransfer.PlayerInside)
            {
                Debug.Log("Player Goes Inside");
            
                foreach (GameObject bottomFloorGameObject in _bottomFloor)
                {
                    bottomFloorGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
                foreach (GameObject kitchenAndMoreGameObject in kitchenWithDoorAndLamp)
                {
                    kitchenAndMoreGameObject.GetComponent<SpriteRenderer>().sortingOrder = 45;
                }
                
                transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount);
                
                player.GetComponentInChildren<Light2D>().intensity = _playerLightIntensity;
            }
            
            DataTransfer.PlayerInsideOrOutside();
            playerSortingGroup.sortingOrder = DataTransfer.PlayerSortingOrder;
        }
    }
}