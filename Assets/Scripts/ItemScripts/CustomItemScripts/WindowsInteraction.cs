using Cinemachine;
using PlayerScripts;
using UnityEngine;

namespace ItemScripts
{
    public class WindowsInteraction : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        private GameObject _lookAtPoint;
        [SerializeField] private GameObject player;
        
        [SerializeField] private bool insideTrigger;
        [SerializeField] private bool isLookingOutside;

        private void Start()
        {
            _lookAtPoint = transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            if (insideTrigger && UserInput.Interact)
            {
                if (isLookingOutside)
                {
                    ReturnCamera();
                }
                else
                {
                    LookOutside();
                }
            }
        }

        private void LookOutside()
        {
            _mainCamera.Follow = _lookAtPoint.transform;
            isLookingOutside = true;
            Debug.Log("Look outside");
        }

        private void ReturnCamera()
        {
            _mainCamera.Follow = player.transform; 
            isLookingOutside = false;
            Debug.Log("Look back at player");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("trigger enter");
            insideTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            insideTrigger = false;
            Debug.Log("trigger exit");
            ReturnCamera();
        }
    }
}
