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
    [SerializeField] AnimationStateController animationStateController;

    public void DirectionInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (direction.x < 0 && !jumpBetweenLanes.currentlyJumping)
        {
            jumpBetweenLanes.StartNewJump(-1);
            // rhythmController.play();
            rhythmController.up();
            if (gameState.GetState() == States.Playing)
                gameState.inputCount++;
        }
        else if (direction.x > 0 && !jumpBetweenLanes.currentlyJumping)
        {
            jumpBetweenLanes.StartNewJump(+1);
            // rhythmController.play();
            rhythmController.down();
            if(gameState.GetState() == States.Playing)
                gameState.inputCount++;
        }
        else if (direction.y < 0 && !jumpBetweenLanes.currentlyJumping && gameState.beerCounter > 0)
        {
            gameState.beerCounter -= 1;
            beerThrowerPlayer.dropBeer();
            rhythmController.throwBeer();
            if (gameState.GetState() == States.Playing)
                gameState.inputCount++;
        }
        else if (direction.y > 0 && gameState.beerCounter > 0)
        {
            animationStateController.drink();
            if (gameState.GetState() == States.Playing)
                gameState.inputCount++;
        }

    }
}
