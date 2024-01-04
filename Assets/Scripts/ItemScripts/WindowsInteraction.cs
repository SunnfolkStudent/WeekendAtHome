using System;
using System.Collections;
using Cinemachine;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace ItemScripts
{
    public class WindowsInteraction : MonoBehaviour
    {
        private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private GameObject lookAtPoint;
        [SerializeField] private GameObject player;

        private void Awake()
        {
            _mainCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        }

        private IEnumerator OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("OnTriggerStay2D sees Player");
            }
            if (!UserInput.Interact)
            {
                yield break;
            }
            else if (UserInput.Interact)
            {
                _mainCamera.Follow = lookAtPoint.transform;
            }
        }
    }
}
