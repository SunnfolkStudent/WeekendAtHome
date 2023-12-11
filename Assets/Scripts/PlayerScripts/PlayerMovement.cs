using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurable Parameters")]
    [SerializeField] private float moveSpeed = 2.5f;

    private PlayerControls _controls;

    // Initialize the player controls
    private void Awake() => _controls = new PlayerControls();

    private void OnEnable() => _controls.Enable();

    private void OnDisable() => _controls.Disable();
    
    // TODO: Update the Update() (general playerMovement), so that colliders on objects still work when framerate is below 60.  
    private void Update()
    {
        // Move the player
        transform.Translate(_controls.Player.Movement.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime));
        // Movement = _controls.Player.Movement.ReadValue<Vector2>();
    }
    
    // public Vector2 Movement { get; private set; }
    
    // The below commented code is the same as the above,
    // but shows the processes behind-the-scenes.

    /*{
          get => movement;
          private set => movement = value;
      } */

}