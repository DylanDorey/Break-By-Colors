using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : MonoBehaviour, IPlayerState
{
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

        playerController.targetPos.y = 3f;
    }
}
