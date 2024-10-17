using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;

    private PlayerData player;
    public Color[] wallColors = new Color[4] { Color.red, Color.blue, Color.green, Color.yellow };

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerData>();
            player.SetNewTargetColor(wallColors[Random.Range(0, wallColors.Length)]);
            player.AddCurrentScore(scoreValue);
        }
    }
}
