using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/28/2024]
 * [Contains all data pertaining to the player]
 */

public class PlayerData : Singleton<PlayerData>
{
    public Color targetColor;
    public Color playerColor;
    private Material playerMat;
    private readonly Color[] wallColors = new Color[4] { Color.red * 20f, Color.blue * 20f, Color.cyan * 20f, Color.yellow * 20f };

    [SerializeField]
    private int currentScore;
    public int highScore;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.gameOver, CreateNewHighScore);
        GameEventBus.Subscribe(GameState.startGame, InitializeTargetColor);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.gameOver, CreateNewHighScore);
        GameEventBus.Unsubscribe(GameState.startGame, InitializeTargetColor);
    }

    private void Start()
    {
        InitializePlayerData();
    }

    private void Update()
    {
        transform.GetChild(0).GetComponent<Renderer>().material.color = playerColor;
    }

    /// <summary>
    /// Initializes all player data values
    /// </summary>
    public void InitializePlayerData()
    {
        playerMat = transform.GetChild(0).GetComponent<Renderer>().material;
        playerMat.EnableKeyword("_EMISSION");

        //color data
        InitializeTargetColor();
    }

    public void InitializeTargetColor()
    {
        if (GameManager.Instance.tutorialSetting)
        {
            targetColor = wallColors[1];
            playerMat.SetColor("_EmissionColor", targetColor);
        }
        else
        {
            targetColor = wallColors[Random.Range(0, wallColors.Length)];
            playerMat.SetColor("_EmissionColor", targetColor);
        }
    }

    /// <summary>
    /// Setter for the current score
    /// </summary>
    /// <param name="addedAmount"> the amount of points to be added to the current score </param>
    public void AddCurrentScore(int addedAmount)
    {
        currentScore += addedAmount;
    }

    /// <summary>
    /// Creates a new high score for the player
    /// </summary>
    public void CreateNewHighScore()
    {
        if(currentScore > highScore)
        {
            highScore = currentScore;
        }

        //highScore = 0;
        currentScore = 0;
    }

    /// <summary>
    /// Getter for the player's high score
    /// </summary>
    /// <returns>the player's highest score</returns>
    public int GetHighScore()
    {
        return highScore;
    }

    /// <summary>
    /// Getter for the player's current score
    /// </summary>
    /// <returns>the player's current session score</returns>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// Returns the players target color
    /// </summary>
    /// <returns>the players current target color</returns>
    public Color GetTargetColor()
    {
        return targetColor;
    }

    /// <summary>
    /// Sets the new target color for the player
    /// </summary>
    /// <param name="newColor"></param>
    public void SetNewTargetColor(Color newColor)
    {
        targetColor = newColor;
        //playerColor = targetColor;

        playerMat.SetColor("_EmissionColor", targetColor);
    }
}
