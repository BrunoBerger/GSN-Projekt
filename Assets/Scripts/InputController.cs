using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] JumpBetweenLanes jumpBetweenLanes;
    [SerializeField] RhythmController rhythmController;
    [SerializeField] BeerThrowerPlayer beerThrowerPlayer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DirectionInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (direction.x < 0 && !jumpBetweenLanes.currentlyJumping)
        {
            jumpBetweenLanes.StartNewJump(-1);
            // rhythmController.play();
            rhythmController.up();
        }
        else if (direction.x > 0 && !jumpBetweenLanes.currentlyJumping)
        {
            jumpBetweenLanes.StartNewJump(+1);
            // rhythmController.play();
            rhythmController.down();
        }
        else if (direction.y < 0 && !jumpBetweenLanes.currentlyJumping && gameState.beerCounter > 0)
        {
            gameState.beerCounter -= 1;
            beerThrowerPlayer.dropBeer();
            rhythmController.throwBeer();
        }

    }
}
