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
    loadGame,
    gameLaunch,
    settingsMenu,
    startGame,
    pauseGame,
    resumeGame,
    gameOver,
    returnToMenu,
    exitGame
}

public class GameManager : Singleton<GameManager>
{
    public bool paused = false;

    public bool audioSetting;
    public bool tutorialSetting;

    public AudioSource buttonAudioSource;
    public AudioSource musicAudioSource;

    public AudioClip buttonClickSound;
    public AudioClip settingsButtonClickSound;

    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.gameLaunch, SetTutorialSetting);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.gameLaunch, SetTutorialSetting);
    }

    private void Start()
    {
        //start the game in the main menu by publishing the menu game event
        GameEventBus.Publish(GameState.loadGame);

        AudioManager.Instance.PlayAudio(musicAudioSource, menuMusic, true);
    }

    /// <summary>
    /// This will start the game Break by Colors from the main menu
    /// </summary>
    public void StartGame()
    {
        //publish the startGame game event
        GameEventBus.Publish(GameState.startGame);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
        AudioManager.Instance.PlayAudio(musicAudioSource, gameplayMusic, true);
    }

    /// <summary>
    /// This will pause the game Break by Colors during gameplay
    /// </summary>
    public void PauseGame()
    {
        //publish the pauseGame game event
        GameEventBus.Publish(GameState.pauseGame);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
    }

    /// <summary>
    /// This will resume the game Break by Colors
    /// </summary>
    public void ResumeGame()
    {
        //publish the pauseGame game event
        GameEventBus.Publish(GameState.resumeGame);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
    }

    /// <summary>
    /// This will return the player to the menu
    /// </summary>
    public void ReturnToMenu()
    {
        //publish the mainMenu game event
        GameEventBus.Publish(GameState.returnToMenu);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
        AudioManager.Instance.PlayAudio(musicAudioSource, menuMusic, true);
    }

    /// <summary>
    /// This will return the player to the menu
    /// </summary>
    public void ReturnToMenuFromSettings()
    {
        //publish the mainMenu game event
        GameEventBus.Publish(GameState.returnToMenu);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
    }

    /// <summary>
    /// This will return the player to the menu
    /// </summary>
    public void ReturnToMenuFromPause()
    {
        //publish the pauseGame game event
        GameEventBus.Publish(GameState.resumeGame);

        //publish the pauseGame game event
        GameEventBus.Publish(GameState.gameOver);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
        AudioManager.Instance.PlayAudio(musicAudioSource, menuMusic, true);
    }

    /// <summary>
    /// This will bring the player to the settings menu
    /// </summary>
    public void GoToSettings()
    {
        //publish the settingsMenu game event
        GameEventBus.Publish(GameState.settingsMenu);

        AudioManager.Instance.PlayAudio(buttonAudioSource, settingsButtonClickSound, false);
    }

    /// <summary>
    /// This will allow the user to close/quit Break by Colors
    /// </summary>
    public void QuitGame()
    {
        //SAVE THE GAME HERE

        //quit the application
        Application.Quit();

        //Application.OpenURL(String URL)
    }

    private void OnApplicationQuit()
    {
        GameEventBus.Publish(GameState.exitGame);
    }

    /// <summary>
    /// This will allow the user to open a player feedback form
    /// </summary>
    public void OpenFeedbackForm(string url)
    {
        Application.OpenURL(url);

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);
    }

    /// <summary>
    /// Changes the audio toggle to true or false when toggled
    /// </summary>
    public void SetAudioSetting()
    {
        //if the audio setting is true/false set it to the opposite value
        Debug.Log("Aud Current: " + audioSetting);

        if(audioSetting)
        {
            audioSetting = false;
            musicAudioSource.Stop();
        }
        else
        {
            audioSetting = true;
            musicAudioSource.Play();
        }

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);

        Debug.Log("Aud Changed To: " + audioSetting);
    }

    /// <summary>
    /// Changes the tutorial toggle to true or false when toggled
    /// </summary>
    public void SetTutorialSetting()
    {
        //if the tutorial setting is true/false set it to the opposite value
        Debug.Log("Tut Current: " + tutorialSetting);

        if (tutorialSetting)
        {
            tutorialSetting = false;
        }
        else
        {
            tutorialSetting = true;
        }

        AudioManager.Instance.PlayAudio(buttonAudioSource, buttonClickSound, false);

        Debug.Log("Tut Changed To: " + tutorialSetting);
    }
}
