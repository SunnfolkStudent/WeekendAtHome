using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurable Parameters")]
    [SerializeField] private float moveSpeed = 5f;

    private PlayerControls _controls;

    private void Awake() => _controls = new PlayerControls();

    private void OnEnable() => _controls.Enable();

    private void OnDisable() => _controls.Disable();
    
    private void Update()
    {
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