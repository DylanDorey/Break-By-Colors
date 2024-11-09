using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNextTrack : MonoBehaviour
{
    [SerializeField]
    private int nextTrackIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TrackSpawner.Instance.pool.SpawnNextTutorialTrack(nextTrackIndex);

            PlayerController.Instance.transform.position = Vector3.zero;
        }
    }
}
