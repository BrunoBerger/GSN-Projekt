using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isJumpingHash, isDroppingBeerHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("IsJumping");
        isDroppingBeerHash = Animator.StringToHash("IsDroppingBeer");
    }

    public void startJump()
    {
        animator.SetBool(isJumpingHash, true);
    }

    public void stopJump()
    {
        animator.SetBool(isJumpingHash, false);
    }

    public void startDrop()
    {
        animator.SetBool(isDroppingBeerHash, true);
    }
    public void stopDrop()
    {
        animator.SetBool(isDroppingBeerHash, false);
    }
}
