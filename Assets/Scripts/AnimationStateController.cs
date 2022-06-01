using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public GameState gameState;
    Animator animator;
    int isJumpingHash, dropBeerHash, animationSpeedHash, dieHash, drinkHash;
    bool pauseTripping = false;
    GameObject bottle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("IsJumping");
        dropBeerHash = Animator.StringToHash("DropBeer");
        animationSpeedHash = Animator.StringToHash("AnimationSpeed");
        dieHash = Animator.StringToHash("Die");
        drinkHash = Animator.StringToHash("Drink");
        bottle = GameObject.Find("lShoulder/lArm/Bottle");
    }

    private void Update()
    {
        animator.SetFloat(animationSpeedHash, (gameState.speed));
    }

    public void startJump()
    {
        animator.SetBool(isJumpingHash, true);
    }

    public void stopJump()
    {
        animator.SetBool(isJumpingHash, false);
    }

    public void dropBeer()
    {
        animator.SetTrigger(dropBeerHash);
    }

    public void die()
    {
        animator.SetTrigger(dieHash);
    }

    public void drink()
    {
        bottle.SetActive(true);
        animator.SetTrigger(drinkHash);
    }

    public void hideBottle() {

        bottle.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" && !pauseTripping)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(PauseTrippingAnimation());
        }
    }

    IEnumerator PauseTrippingAnimation()
    {
        pauseTripping = true;
        yield return new WaitForSeconds(1);
        pauseTripping = false;
    }
}
