using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWall : MonoBehaviour
{
    [SerializeField]
    private Material[] wallMaterials;

    public TutorialWall PreviousTrack;

    [SerializeField]
    public bool willMatch;

    [SerializeField]
    private bool _othersRandom;

    [SerializeField]
    private bool _othersConstant;

    [SerializeField]
    private Color _constantColor;

    [SerializeField]
    public int ScoreValue;

    [SerializeField]
    private GameObject[] _blocks;

    [SerializeField]
    public Color NextTargetColor;

    [SerializeField]
    private Color _thisWallColor;

    [SerializeField]
    private int _targetBlockIndex;

    [HideInInspector]
    public GameObject Track;

    [HideInInspector]
    public AudioClip[] wallBreakSounds;

    [HideInInspector]
    public AudioSource wallAudioSource;

    private Renderer[] wallRenderers;

    private void Start()
    {
        Track = transform.parent.transform.parent.gameObject;
        wallRenderers = new Renderer[6];

        for (int i = 0; i < _blocks.Length; i++)
        {
            if (_blocks[i] != null)
            {
                wallRenderers[i] = transform.GetChild(i).transform.GetChild(0).GetComponent<Renderer>();
            }
        }

        if(willMatch)
        {
            for (int i = 0; i < _blocks.Length; i++)
            {
                if (_blocks[i] != null)
                {
                    if (i == _targetBlockIndex)
                    {
                        wallRenderers[i].sharedMaterial = wallMaterials[ConvertColorToIndex(_thisWallColor)];
                        _blocks[i].GetComponent<TutorialBlock>().ThisWallColor = _thisWallColor;
                    }
                    else
                    {
                        if (_othersRandom)
                        {
                            int randomColorIndex = ConvertColorToIndex(_thisWallColor);

                            while (randomColorIndex == ConvertColorToIndex(_thisWallColor))
                            {
                                randomColorIndex = Random.Range(0, 4);
                            }

                            wallRenderers[i].sharedMaterial = wallMaterials[randomColorIndex];
                            _blocks[i].GetComponent<TutorialBlock>().ThisWallColor = ConvertMaterialToColor(wallRenderers[i]);
                        }
                        else if (_othersConstant)
                        {
                            wallRenderers[i].sharedMaterial = wallMaterials[ConvertColorToIndex(_constantColor)];
                            _blocks[i].GetComponent<TutorialBlock>().ThisWallColor = ConvertMaterialToColor(wallRenderers[i]);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < _blocks.Length; i++)
            {
                if (_blocks[i] != null)
                {
                    int randomColorIndex = Random.Range(0, 4);
                    wallRenderers[i].sharedMaterial = wallMaterials[randomColorIndex];
                    _blocks[i].GetComponent<TutorialBlock>().ThisWallColor = ConvertMaterialToColor(wallRenderers[i]);
                }
            }
        }

        wallBreakSounds = AudioManager.Instance.wallBreakSounds;
        wallAudioSource = AudioManager.Instance.wallAudioSource;
    }

    private int ConvertColorToIndex(Color c)
    {
        if (c == Color.red)
        {
            return 0;
        }
        else if (c == Color.blue)
        {
            return 1;
        }
        else if (c == Color.cyan)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private Color ConvertMaterialToColor(Renderer wR)
    {
        if (wR.sharedMaterial == wallMaterials[0])
        {
            return Color.red;
        }
        else if (wR.sharedMaterial == wallMaterials[1])
        {
            return Color.blue;
        }
        else if (wR.sharedMaterial == wallMaterials[2])
        {
            return Color.cyan;
        }
        else
        {
            return Color.yellow;
        }
    }
}
