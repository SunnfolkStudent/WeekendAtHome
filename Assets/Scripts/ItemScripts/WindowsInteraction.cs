using System;
using Cinemachine;
using PlayerScripts;
using UnityEngine;

namespace ItemScripts
{
    public class WindowsInteraction : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        [SerializeField] private GameObject lookAtPoint;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            if (UserInput.HoldInteract) { mainCamera.Follow = lookAtPoint.transform; }
            else { mainCamera.Follow = other.transform; }
        }
    }
}
