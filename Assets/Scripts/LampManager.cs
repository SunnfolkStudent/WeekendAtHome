using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class LampManager : MonoBehaviour
{
    [SerializeField] private Light2D lampLight;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lampOnSfx, lampOffSfx;
    
    private bool _triggerActive;
    
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }

    // Update is called once per frame
    private void Update()
    {
        if (!_triggerActive || !UserInput.Interact)
            return;
        
        if (DataTransfer.LampOn)
        {
            DataTransfer.LampOn = false;
            lampLight.intensity = 0;
            audioSource.PlayOneShot(lampOffSfx);
        }
        else if (!DataTransfer.LampOn)
        {
            DataTransfer.LampOn = true;
            lampLight.intensity = 1;
            audioSource.PlayOneShot(lampOnSfx);
        }
        DataTransfer.TurnLampOnOrOff();
    }
}
