using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject menuScreen;
    public GameObject gameScreen;
    public GameObject gameOverScreen;

    /// <summary>
    /// PLAYTEST 1 ONLY
    /// </summary>
    public Image targetColor;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.mainMenu, EnableMenuUI);
        GameEventBus.Subscribe(GameState.startGame, EnablePlayingUI);
        GameEventBus.Subscribe(GameState.startGame, EnableGameOverUI);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.mainMenu, EnableMenuUI);
        GameEventBus.Unsubscribe(GameState.startGame, EnablePlayingUI);
        GameEventBus.Unsubscribe(GameState.startGame, EnableGameOverUI);
    }

    private void Update()
    {
        targetColor.color = PlayerData.Instance.GetTargetColor();
    }

    /// <summary>
    /// enables the menu UI
    /// </summary>
    private void EnableMenuUI()
    {
        //disable the playing screen and game over screen, but enable the menu screen
        SetDisplayScreen(true, false, false);
    }

    /// <summary>
    /// enables the playing game screen
    /// </summary>
    private void EnablePlayingUI()
    {
        //disable the menu and game over screen, but enable the playing screen
        SetDisplayScreen(false, true, false);
    }

    /// <summary>
    /// enables the game over screen
    /// </summary>
    private void EnableGameOverUI()
    {
        //disable the menu and playing, but enable the game over screen
        SetDisplayScreen(false, false, true);
    }

    /// <summary>
    /// enables and disables the correct screen at runtime
    /// </summary>
    /// <param name="menu"> sets the menu screen on or off </param>
    /// <param name="game"> sets the game screen on or off </param>
    /// <param name="over"> sets the game over screen on or off </param>
    private void SetDisplayScreen(bool menu, bool game, bool over)
    {
        menuScreen.SetActive(menu);
        gameScreen.SetActive(game);
        gameOverScreen.SetActive(over);
    }
}
