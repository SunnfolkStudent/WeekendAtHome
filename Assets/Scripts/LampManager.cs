using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using PlayerInput = PlayerScripts.PlayerInput;

public class LampManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private DataTransfer dataTransfer;
    
    public bool lampOn;
    private bool _canTurnOn;
    
    [SerializeField] private PlayerInput input;
    [SerializeField] private Light2D lampLight;
    
    // Start is called before the first frame update
    private void Start()
    {
        input = player.GetComponent<PlayerInput>();
        dataTransfer = dataTransfer.GetComponent<DataTransfer>();
        lampOn = dataTransfer.lampOn;
    }

    private void OnTriggerEnter2D(Collider2D other) { _canTurnOn = true; }
    private void OnTriggerExit2D(Collider2D other) { _canTurnOn = false; }

    // Update is called once per frame
    private void Update()
    {
        lampOn = dataTransfer.lampOn;
        
        if (!_canTurnOn || !input.interact)
            return;
        
        if (lampOn)
        {
            lampOn = false;
            lampLight.intensity = 0;
        }
        else if (!lampOn)
        {
            lampOn = true;
            lampLight.intensity = 1;
        }
        dataTransfer.TurnLampOnOrOff();
    }
}
