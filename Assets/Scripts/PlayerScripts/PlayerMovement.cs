using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Configurable Parameters")]
        [SerializeField] private float moveSpeed = 2.25f;
        public bool playerCanMove;
        
        public Animator anim;
        [SerializeField] private PlayerInput input;
        [SerializeField] private DataTransfer dataTransfer;
        [SerializeField] private Rigidbody2D rb;
    
        private Vector2 _movement;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private void Start()
        {
            input = GetComponent<PlayerInput>();
            dataTransfer = dataTransfer.GetComponent<DataTransfer>();
            playerCanMove = dataTransfer.playerCanMove = true;
        }

        private void Update()
        {
            playerCanMove = dataTransfer.playerCanMove;
            if (!playerCanMove)
                return;
            
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            // Move the player
            // transform.Translate(_controls.Player.Movement.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime));
            // Movement = _controls.Player.Movement.ReadValue<Vector2>();

        
            if (_movement == Vector2.zero || !playerCanMove)
            {
                anim.Play("Player_Idle_Normal");
            }
            else
            {
                anim.Play("Movement");
                anim.SetFloat("Horizontal", _movement.x);
                anim.SetFloat("Vertical", _movement.y);
            } 

            /*
        if (_movement.x != 0 && _movement.y == 0 && canMove)
        {
           anim.transform.localScale = new Vector3(-_movement.x, 1f, 1f);
        }
        else
        {
            anim.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        */
        }

        private void FixedUpdate()
        {
            if (playerCanMove)
                rb.MovePosition(rb.position + _movement.normalized * (moveSpeed * Time.fixedDeltaTime));
        }
    
        /* public void SetCanMove(bool PlayerCanMove)
        {
            PlayerCanMove = PlayerCanMove;
        } */

    }
}