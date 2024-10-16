using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    //the UI screen objects
    public GameObject menuScreen;
    public GameObject gameScreen;
    public GameObject pausedScreen;
    public GameObject gameOverScreen;

    //player's target color
    public Image targetColor;

    //player's score
    public TextMeshProUGUI score;


    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.mainMenu, EnableMenuUI);
        GameEventBus.Subscribe(GameState.startGame, EnablePlayingUI);
        GameEventBus.Subscribe(GameState.pauseGame, EnablePausedUI);
        GameEventBus.Subscribe(GameState.resumeGame, EnablePlayingUI);
        GameEventBus.Subscribe(GameState.gameOver, EnableGameOverUI);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.mainMenu, EnableMenuUI);
        GameEventBus.Unsubscribe(GameState.startGame, EnablePlayingUI);
        GameEventBus.Unsubscribe(GameState.pauseGame, EnablePausedUI);
        GameEventBus.Unsubscribe(GameState.resumeGame, EnablePlayingUI);
        GameEventBus.Unsubscribe(GameState.gameOver, EnableGameOverUI);
    }

    private void Update()
    {
        targetColor.color = PlayerData.Instance.GetTargetColor();
        score.text = PlayerData.Instance.GetCurrentScore().ToString();
    }

    /// <summary>
    /// enables the menu UI
    /// </summary>
    private void EnableMenuUI()
    {
        //disable the playing screen, game over screen, and paused screen but enable the menu screen
        SetDisplayScreen(true, false, false, false);
    }

    /// <summary>
    /// enables the playing game screen
    /// </summary>
    private void EnablePlayingUI()
    {
        //disable the menu, game over screen, and paused screen but enable the playing screen
        SetDisplayScreen(false, true, false, false);
    }

    /// <summary>
    /// enables the paused screen
    /// </summary>
    private void EnablePausedUI()
    {
        //disable the menu, playing, and game over screen but enable the paused screen
        SetDisplayScreen(false, false, true, false);
    }

    /// <summary>
    /// enables the game over screen
    /// </summary>
    private void EnableGameOverUI()
    {
        //disable the menu, playing, and paused screens, but enable the game over screen
        SetDisplayScreen(false, false, false, true);
    }

    /// <summary>
    /// enables and disables the correct screen at runtime
    /// </summary>
    /// <param name="menu"> sets the menu screen on or off </param>
    /// <param name="game"> sets the game screen on or off </param>
    /// <param name="over"> sets the game over screen on or off </param>
    private void SetDisplayScreen(bool menu, bool game, bool paused, bool over)
    {
        menuScreen.SetActive(menu);
        gameScreen.SetActive(game);
        pausedScreen.SetActive(paused);
        gameOverScreen.SetActive(over);
    }
}
