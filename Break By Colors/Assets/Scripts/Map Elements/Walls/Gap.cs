using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;

    private PlayerData player;
    private readonly Color[] wallColors = new Color[4] { Color.red * 20f, Color.blue * 20f, Color.cyan * 20f, Color.yellow * 20f };

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
