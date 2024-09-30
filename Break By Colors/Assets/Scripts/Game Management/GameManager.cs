using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/15/2024]
 * [This will manage all game functions such as initializing, starting, returning to menu, and quiting]
 */

//the various game states
public enum GameState
{
    mainMenu,
    initializeGame,
    startGame,
    gameOver
}

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        //start the game in the main menu by publishing the menu game event
        //GameEventBus.Publish(GameState.mainMenu);
    }

    /// <summary>
    /// Initializes all game specific values
    /// </summary>
    public void InitializeGame()
    {
        //publish the initializeGame game event
        //GameEventBus.Publish(GameState.initializeGame);
    }

    /// <summary>
    /// This will start the game Break by Colors from the main menu
    /// </summary>
    public void StartGame()
    {
        //publish the startGame game event
        GameEventBus.Publish(GameState.startGame);
    }

    /// <summary>
    /// This will allow the user to close/quit Break by Colors
    /// </summary>
    public void QuitGame()
    {
        //quit the application
        Application.Quit();
    }
}
