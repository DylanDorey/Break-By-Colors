using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWallAbsolute : MonoBehaviour
{
    public bool willMatch;
    public GameObject track;

    [SerializeField]
    private int scoreValue;

    private AudioClip[] wallBreakSounds;
    private AudioSource wallAudioSource;

    public Material[] wallMaterials;
    public Color thisWallColor;

    private Renderer wallRenderer;
    private PlayerData player;

    private void Start()
    {
        wallRenderer = transform.GetChild(0).GetComponent<Renderer>();

        wallRenderer.sharedMaterial = wallMaterials[3];

        thisWallColor = Color.yellow * 20f;

        wallBreakSounds = AudioManager.Instance.wallBreakSounds;
        wallAudioSource = AudioManager.Instance.wallAudioSource;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerData>();

            if (willMatch)
            {
                if (player.GetTargetColor() == thisWallColor)
                {
                    player.SetNewTargetColor(Color.yellow * 20f);
                    player.AddCurrentScore(scoreValue);
                    AudioManager.Instance.PlayAudio(wallAudioSource, wallBreakSounds[Random.Range(0, wallBreakSounds.Length)], false);
                }
                else
                {
                    PlayerController.Instance.ResetPlayerPosition();
                    track.transform.position += new Vector3(0f, 0f, 45f);
                    //transform.parent.transform.parent.transform.position = Vector3.zero;
                }
            }
            else
            {
                PlayerController.Instance.transform.position = Vector3.zero;
                track.transform.position += new Vector3(0f, 0f, 45f);
            }
        }
    }
}
