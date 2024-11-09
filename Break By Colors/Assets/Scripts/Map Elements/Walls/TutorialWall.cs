using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWall : MonoBehaviour
{
    public bool willMatch;
    public GameObject track;

    [SerializeField]
    private int scoreValue;

    private AudioClip[] wallBreakSounds;
    private AudioSource wallAudioSource;

    private readonly Color[] wallColors = new Color[4] { Color.red * 20f, Color.blue * 20f, Color.cyan * 20f, Color.yellow * 20f };
    public Material[] wallMaterials;
    public Color thisWallColor;

    private Renderer wallRenderer;
    private PlayerData player;

    private void Start()
    {
        wallRenderer = transform.GetChild(0).GetComponent<Renderer>();
        wallRenderer.sharedMaterial = wallMaterials[Random.Range(0, 4)];
        thisWallColor = InitializeCurrentColor();
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
                    player.SetNewTargetColor(wallColors[Random.Range(0, wallColors.Length)]);
                    player.AddCurrentScore(scoreValue);
                    AudioManager.Instance.PlayAudio(wallAudioSource, wallBreakSounds[Random.Range(0, wallBreakSounds.Length)], false);
                }
                else
                {
                    PlayerController.Instance.ResetPlayerPosition();
                    transform.parent.transform.parent.transform.position = Vector3.zero;
                }
            }
            else
            {
                PlayerController.Instance.transform.position = Vector3.zero;
                track.transform.position = Vector3.zero;
            }
        }
    }

    private Color InitializeCurrentColor()
    {
        if (wallRenderer.sharedMaterial == wallMaterials[0])
        {
            return Color.red * 20f;
        }
        else if (wallRenderer.sharedMaterial == wallMaterials[1])
        {
            return Color.blue * 20f;
        }
        else if (wallRenderer.sharedMaterial == wallMaterials[2])
        {
            return Color.cyan * 20f;
        }
        else
        {
            return Color.yellow * 20f;
        }
    }
}
