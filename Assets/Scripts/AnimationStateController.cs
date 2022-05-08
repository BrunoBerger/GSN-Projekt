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

    // Update is called once per frame
    void Update()
    {
        bool jumpPressed = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
        bool dropPressed = Input.GetKeyDown(KeyCode.Space);

        if (jumpPressed)
            animator.SetBool(isJumpingHash, true);

        if (!jumpPressed)
            animator.SetBool(isJumpingHash, false);

        if (dropPressed)
            animator.SetBool(isDroppingBeerHash, true);

        if (!dropPressed)
            animator.SetBool(isDroppingBeerHash, false);
        
    }
}
