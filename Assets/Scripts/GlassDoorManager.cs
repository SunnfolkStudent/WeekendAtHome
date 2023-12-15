using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDoorManager : MonoBehaviour
{

    private Animator animator;
    public bool isOpen;

    public bool isPlaying;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    public void OpenOrCloseDoor()
    {
        if (isPlaying)
            return;
        
        if (!isOpen) { animator.Play("GlassDoorSliding"); }
        else { animator.Play("GlassDoorSlidingClosed"); }

        isOpen = !isOpen;
        
        return;
    }

    public void SetAnimationToPlayingOrNot() { isPlaying = !isPlaying; }

    public void AllowMovementAgain() { GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().canMove = true; }
}
