using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class WindowsInteraction : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera mainCamera;

    [SerializeField] private GameObject lookAtPoint;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Keyboard.current.eKey.isPressed) { mainCamera.Follow = lookAtPoint.transform; }
        else { mainCamera.Follow = other.transform; }

        return;
    }
}
