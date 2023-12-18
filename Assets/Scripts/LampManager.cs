using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LampManager : MonoBehaviour
{

    private bool isTurnedOn;
    private bool canTurnOn;

    [SerializeField] private Light2D lampLight;
    
    // Start is called before the first frame update
    void Start()
    {
        isTurnedOn = true;
    }

    private void OnTriggerEnter2D(Collider2D other) { canTurnOn = true; }
    private void OnTriggerExit2D(Collider2D other) { canTurnOn = false; }

    // Update is called once per frame
    void Update()
    {
        if (!canTurnOn)
            return;
        
        if (Keyboard.current.eKey.wasPressedThisFrame && isTurnedOn)
        {
            isTurnedOn = false;
            lampLight.intensity = 0;
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame && !isTurnedOn)
        {
            isTurnedOn = true;
            lampLight.intensity = 1;
        }
    }
}
