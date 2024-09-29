using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;
    public Color[] wallColors = new Color[4] { Color.red, Color.blue, Color.green, Color.yellow };

    [SerializeField]
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

            if (player.targetColor == thisMaterial.color)
            {
                //player.AddScore(scoreValue);
                player.SetNewTargetColor(wallColors[Random.Range(0, wallColors.Length)]);
                player.SetColorsMatched();
                gameObject.SetActive(false);
            }
            else
            {
                player.gameObject.GetComponent<PlayerController>().ResetPlayerPosition();
            }
        }
    }
}