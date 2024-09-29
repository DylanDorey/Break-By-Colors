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
    private Color playerColor;
    private int colorsMatched = 0;

    private void Update()
    {
        transform.GetChild(0).GetComponent<Renderer>().material.color = playerColor;
    }

    public void InitializePlayerData()
    {
        //color data
        playerColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        targetColor = Color.red;
        playerColor = targetColor;
    }

    public void SetNewTargetColor(Color newColor)
    {
        targetColor = newColor;
        playerColor = targetColor;
    }

    public void SetColorsMatched()
    {
        colorsMatched += 1;
    }
}
