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
    settingsMenu,
    initializeGame,
    startGame,
    pauseGame,
    resumeGame,
    gameOver,
    returnToMenu
}

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        //start the game in the main menu by publishing the menu game event
        GameEventBus.Publish(GameState.mainMenu);
    }

    /// <summary>
    /// Initializes all game specific values
    /// </summary>
    public void InitializeGame()
    {
        //publish the initializeGame game event
        GameEventBus.Publish(GameState.initializeGame);
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
    /// This will pause the game Break by Colors during gameplay
    /// </summary>
    public void PauseGame()
    {
        //publish the pauseGame game event
        GameEventBus.Publish(GameState.pauseGame);
    }

    /// <summary>
    /// This will resume the game Break by Colors
    /// </summary>
    public void ResumeGame()
    {
        //publish the pauseGame game event
        GameEventBus.Publish(GameState.resumeGame);
    }

    /// <summary>
    /// This will return the player to the menu
    /// </summary>
    public void ReturnToMenu()
    {
        //publish the mainMenu game event
        GameEventBus.Publish(GameState.returnToMenu);
    }

    /// <summary>
    /// This will bring the player to the settings menu
    /// </summary>
    public void GoToSettings()
    {
        //publish the settingsMenu game event
        GameEventBus.Publish(GameState.settingsMenu);
    }

    /// <summary>
    /// This will allow the user to close/quit Break by Colors
    /// </summary>
    public void QuitGame()
    {
        //SAVE THE GAME HERE

        //quit the application
        Application.Quit();
    }
}
