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
    private Color targetColor;
    private Color playerColor;
    private int colorsMatched = 0;
    private readonly Color[] wallColors = new Color[4] { Color.red, Color.blue, Color.green, Color.yellow };

    private int currentScore;
    private int highScore;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.gameOver, ResetCurrentScore);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.gameOver, ResetCurrentScore);
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
        //color data
        playerColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        targetColor = wallColors[Random.Range(0, wallColors.Length)];
        playerColor = targetColor;
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
    /// Setter for the high score
    /// </summary>
    /// <param name="newHighScore">the new high score of the player</param>
    public int SetHighScore(int newHighScore)
    {
        highScore = newHighScore;

        return highScore;
    }

    /// <summary>
    /// Creates a new high score for the player
    /// </summary>
    /// <param name="potentialNewHighScore">the potential new high score that the player just earned (current score)</param>
    public void CreateNewHighScore(int potentialNewHighScore)
    {
        if(potentialNewHighScore > highScore)
        {
            SetHighScore(potentialNewHighScore);
        }
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
    /// Increases the colors matched variable
    /// </summary>
    public void SetColorsMatched()//int increaseByValue)
    {
        colorsMatched += 1;
    }

    /// <summary>
    /// Returns the amount of colors matched
    /// </summary>
    /// <returns>the amount of colors matched by the player </returns>
    public int GetColorsMatched()
    {
        return colorsMatched;
    }

    /// <summary>
    /// Sets the new target color for the player
    /// </summary>
    /// <param name="newColor"></param>
    public void SetNewTargetColor(Color newColor)
    {
        targetColor = newColor;
        playerColor = targetColor;
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }
}
