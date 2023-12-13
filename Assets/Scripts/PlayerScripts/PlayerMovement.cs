using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurable Parameters")]
    [SerializeField] private float moveSpeed = 2.5f;

    public Animator anim;

    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 _movement;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        // Move the player
        // transform.Translate(_controls.Player.Movement.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime));
        // Movement = _controls.Player.Movement.ReadValue<Vector2>();
        
        anim.SetFloat(Horizontal, _movement.x);
        anim.SetFloat(Vertical, _movement.y);
        anim.SetFloat(Speed, _movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }

}