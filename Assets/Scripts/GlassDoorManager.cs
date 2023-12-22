using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GlassDoorManager : MonoBehaviour
{
    [SerializeField] private float slideTimer;
    
    public bool glassDoorOpen;
    public bool triggerActive;
    public bool playerCanMove;
    
    public GameObject player;
    [SerializeField] private DataTransfer dataTransfer;
    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerInput _input;
    [SerializeField] private AudioClip doorClosing, doorOpening;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _input = player.GetComponentInChildren<PlayerInput>();
        dataTransfer = dataTransfer.GetComponent<DataTransfer>();
    }

    private void Update()
    {
        glassDoorOpen = dataTransfer.glassDoorOpen;
        playerCanMove = dataTransfer.playerCanMove;
        slideTimer += Time.deltaTime;
        
        if (!triggerActive)
            return;
        
        // if animation is done, and player is within triggerBox and presses Interact, continue downwards.
        if (!(slideTimer > 2.25f) || !_input.interact) 
            return;
        
        // Disable playerInput while glassDoor opens
        // dataTransfer.playerCanMove = false;
            
        // If glassDoor is Open, close it. If glassDoor is Closed, open it.
        switch (glassDoorOpen)
        {
            case false:
                Debug.Log("DoorIsOpening");
                _animator.Play("GlassDoorSliding");
                _audioSource.PlayOneShot(doorOpening);
                glassDoorOpen = true;
                break;
            case true:
                Debug.Log("DoorIsClosing");
                _animator.Play("GlassDoorSlidingClosed");
                _audioSource.PlayOneShot(doorClosing);
                glassDoorOpen = false;
                break;
        }
        // Reset the timer to be 0.
        slideTimer = -Time.deltaTime;
        
        // Return controls back to the player once animation ends.
        /* if (!playerCanMove && slideTimer > 2.25f)
        {
            playerCanMove = true;
        } */
    }

    private void OnTriggerEnter2D(Collider2D other) { triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { triggerActive = false; }

    
    /* public void SetAnimationToPlayingOrNot()
    {
        isPlaying = !isPlaying;
    }

    public void AllowMovementAgain()
    {
        DataTransfer.PlayerCanMove = true;
    }

    public void OpenOrCloseDoor()
    {
        if (isPlaying)
        {
            _input.OnDisable();
            Debug.Log("DoorIsMoving");
            return;
        }

        if (!GlassDoorOpen && triggerActive && _input.interact)
        {
            Debug.Log("DoorIsOpening");
            _animator.Play("GlassDoorSliding");
            _audioSource.PlayOneShot(doorOpening);
            SetAnimationToPlayingOrNot();
        }
        else if (GlassDoorOpen && triggerActive && _input.interact)
        {
            Debug.Log("DoorIsClosing");
            _animator.Play("GlassDoorSlidingClosed");
            _audioSource.PlayOneShot(doorClosing);
            SetAnimationToPlayingOrNot();
        }

        GlassDoorOpen = !GlassDoorOpen;
        _input.OnEnable();
    } */
}
