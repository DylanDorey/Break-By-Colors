using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [10/13/2024]
 * [Handles the logic for the PlayerMoveState]
 */

public class PlayerMoveState : MonoBehaviour, IPlayerState
{
    //The desired turn direction
    private float moveDirection;

    //reference to the player controller script
    private PlayerController playerController;

    /// <summary>
    /// Handles the player controller turn state switch
    /// </summary>
    /// <param name="playerController"> the player controller that is changing states </param>
    public void Handle(PlayerController _playerController)
    {
        //if playerController is empty/null
        if (!playerController)
        {
            //initialize playerController to the incoming/passed in playerContoller
            playerController = _playerController;
        }

        //set moveDirection equal to the players current move direction (casted as a float value)
        moveDirection = (float)playerController.CurrentMoveDirection;

        //set the target position of the player to the current players postiion plus how much distance is left between them and the desired location
        playerController.targetPos += playerController.transform.position + CalculateDistanceToMove();
    }

    /// <summary>
    /// Calculates the distance the player controller needs to move when a left/rigth button is pressed
    /// </summary>
    /// <returns> the movement distance left to move </returns>
    private Vector3 CalculateDistanceToMove()
    {
        float dist = moveDirection - playerController.transform.position.x;

        //return the distance left for the player to move
        return new Vector3(dist, 0f, 0f);
    }
}
