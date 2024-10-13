using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [10/13/2024]
 * [Sets the state context for the player]
 */

public class PlayerStateContext : MonoBehaviour
{
    //public property for the current state of the player
    public IPlayerState CurrentState
    {
        get; set;
    }

    //private playerController reference
    private readonly PlayerController playerController;

    //constructor for playerController
    public PlayerStateContext(PlayerController _playerController)
    {
        //initialize _playerController with the passed in playerController parameter
        playerController = _playerController;
    }

    /// <summary>
    /// Handles the transition of switching the player controller's state without any parameters
    /// </summary>
    public void Transition()
    {
        //handle the basic state switch
        CurrentState.Handle(playerController);
    }

    /// <summary>
    /// Handles the transition of switching the player controller's state with a state parameter
    /// </summary>
    /// <param name="state"></param>
    public void Transition(IPlayerState state)
    {
        //set the new incoming state
        CurrentState = state;

        //apply the state to the player controller object
        CurrentState.Handle(playerController);
    }
}
