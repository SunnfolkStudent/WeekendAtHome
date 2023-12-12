using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    // The below code t
    public static readonly int WalkRight = Animator.StringToHash("Walk_Right");
    public static readonly int WalkLeft = Animator.StringToHash("Walk_Left");
    public static readonly int WalkUp = Animator.StringToHash("Walk_Up");
    public static readonly int WalkDown = Animator.StringToHash("Walk_Down");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 velocity = rb.velocity.normalized;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        
        if (angle > -45f && angle <= 45f)
        {
            PlayAnimation(WalkRight);
        }
        else if (angle > 45f && angle <= 135f)
        {
            PlayAnimation(WalkUp);
        }
        else if (angle > 135f || angle <= -135f)
        {
            PlayAnimation(WalkLeft);
        }
        else if (angle < -45f && angle >= -135f)
        {
            PlayAnimation(WalkDown);
        }
        // TODO: If player escapes chase range, go back to Idle animation, and wait before returning to original spawn area.
    }

    void PlayAnimation(int direction)
    {
        animator.Play(direction);
    }
}