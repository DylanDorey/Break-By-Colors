using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWallRandom : MonoBehaviour
{
    public GameObject track;

    private readonly Color[] wallColors = new Color[4] { Color.red * 20f, Color.blue * 20f, Color.cyan * 20f, Color.yellow * 20f };
    public Material[] wallMaterials;
    public Color thisWallColor;

    private Renderer wallRenderer;

    private void Start()
    {
        wallRenderer = transform.GetChild(0).GetComponent<Renderer>();

        wallRenderer.sharedMaterial = wallMaterials[Random.Range(0, 4)];
        thisWallColor = InitializeCurrentColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.transform.position = Vector3.zero;
            track.transform.position += new Vector3(0f, 0f, 45f);
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

    private void InitializeWallColor()
    {
        if (thisWallColor == Color.red * 20f)
        {
            wallRenderer.sharedMaterial = wallMaterials[0];
        }
        else if (thisWallColor == Color.blue * 20f)
        {
            wallRenderer.sharedMaterial = wallMaterials[1];
        }
        else if (thisWallColor == Color.cyan * 20f)
        {
            wallRenderer.sharedMaterial = wallMaterials[2];
        }
        else
        {
            wallRenderer.sharedMaterial = wallMaterials[3];
        }
    }
}
