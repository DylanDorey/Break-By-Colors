using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNextTrack : MonoBehaviour
{
    [SerializeField]
    private int nextTrackIndex;

    [SerializeField]
    private GameObject tutUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TrackSpawner.Instance.pool.SpawnGameTrack();

            PlayerController.Instance.transform.position = Vector3.zero;

            GameManager.Instance.tutorialSetting = false;
            UIManager.Instance.tutorialToggle.isOn = false;

            foreach (GameObject screen in UIManager.Instance.tutorialScreens)
            {
                screen.SetActive(false);
            }

            PlayerData.Instance.ResetCurrentScore();
        }
    }
}
