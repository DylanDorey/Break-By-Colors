using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;

    private AudioClip gapSound;
    private AudioSource gapAudioSource;

    private PlayerData player;
    private readonly Color[] wallColors = new Color[4] { Color.red * 20f, Color.blue * 20f, Color.cyan * 20f, Color.yellow * 20f };

    private void Start()
    {
        gapAudioSource = AudioManager.Instance.gapAudioSource;
        gapSound = AudioManager.Instance.gapSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio(gapAudioSource, gapSound, false);
            player = other.gameObject.GetComponent<PlayerData>();
            player.plusOneParticle.Play();
            player.SetNewTargetColor(wallColors[Random.Range(0, wallColors.Length)]);
            player.AddCurrentScore(scoreValue);

            TrackSpawner.Instance.TrackSpeedUpdate(1.3f);
        }
    }
}
