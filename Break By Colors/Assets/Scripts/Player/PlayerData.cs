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
        targetColor = Color.red;
        playerColor = targetColor;
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
}
