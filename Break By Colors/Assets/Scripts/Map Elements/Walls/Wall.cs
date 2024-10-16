using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [10/13/2024]
 * [A wall of a particular color that gives the player score when the player correctly matches with it]
 */

public class Wall : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;
    public Color[] wallColors = new Color[4] { Color.red, Color.blue, Color.green, Color.yellow };

    private Material thisMaterial;
    private PlayerData player;

    private void Start()
    { 
        thisMaterial = transform.GetChild(0).GetComponent<Renderer>().material;
        thisMaterial.color = wallColors[Random.Range(0, wallColors.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerData>();

            if (player.GetTargetColor() == thisMaterial.color)
            {
                //player.AddScore(scoreValue);
                player.SetNewTargetColor(wallColors[Random.Range(0, wallColors.Length)]);
                player.SetColorsMatched();
                TrackEventBus.Publish(TrackEvent.changeSpeed);
            }
            else
            {
                //Set the game to game over
                //player.gameObject.GetComponent<PlayerController>().ResetPlayerPosition();
                GameEventBus.Publish(GameState.gameOver);
            }
        }
    }
}
