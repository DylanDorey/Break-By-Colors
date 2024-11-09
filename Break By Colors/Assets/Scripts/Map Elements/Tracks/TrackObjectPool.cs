using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/15/2024]
 * [Object Pool of track game objects]
 */

public class TrackObjectPool : MonoBehaviour
{
    //the max size of tracks in the pool
    public int maxPoolSize;
    public GameObject track50Prefab;
    private GameObject trackParent;
    private GameObject tutorialTrackParent;
    public GameObject[] tutorialTrackPrefabs;

    public List<GameObject> trackPool;
    public List<GameObject> tutorialTracks;

    private int wallSpawnChance;

    private void Start()
    {
        maxPoolSize = TrackSpawner.Instance.trackLength;
    }

    public void PopulateGameTrackPool()
    {
        //create new parent track pool game object
        trackParent = new GameObject("TrackObjectPool");

        //if the max pool size is not initialized
        if(maxPoolSize <= 0)
        {
            //initialize it to the track length
            maxPoolSize = TrackSpawner.Instance.trackLength;
        }

        wallSpawnChance = TrackSpawner.Instance.wallSpawnChance;

        for (int index = 0; index < maxPoolSize; index++)
        { 
            GameObject track = Instantiate(track50Prefab, Vector3.zero, Quaternion.identity);
            trackPool.Add(track);

            track.transform.parent = trackParent.transform;
            track.GetComponent<Track>().InitializeTrack(TrackSpawner.Instance.trackSize, TrackSpawner.Instance.startSpeed, wallSpawnChance);
            track.name = "Track" + index;

            if(index == maxPoolSize - 1)
            {
                track.GetComponent<Track>().nextTrack = trackPool[0];
            }

            if(index != 0)
            {
                trackPool[index-1].GetComponent<Track>().nextTrack = track;
            }
        }
    }

    public void SpawnGameTrack()
    {
        if(trackParent == null)
        {
            PopulateGameTrackPool();
        }

        float spawnPoint = (TrackSpawner.Instance.trackSize * TrackSpawner.Instance.trackLength) - (TrackSpawner.Instance.trackSize);

        for (int index = 0; index < maxPoolSize; index++)
        {
            GameObject track = trackPool[index];

            track.SetActive(true);

            track.transform.position = new Vector3(0f, -0.7f, spawnPoint);
            spawnPoint -= TrackSpawner.Instance.trackSize;
        }
    }

    public void SpawnTutorialTrack()
    {
        //create new parent tutorial track pool game object
        tutorialTrackParent = new GameObject("TutorialTrackParent");

        for (int index = 0; index < tutorialTrackPrefabs.Length; index++)
        {
            GameObject track = Instantiate(tutorialTrackPrefabs[index], Vector3.zero, Quaternion.identity);
            tutorialTracks.Add(track);
            track.transform.parent = tutorialTrackParent.transform;
            track.SetActive(false);
        }

        tutorialTracks[0].SetActive(true);
    }

    public void SpawnNextTutorialTrack(int index)
    {
        tutorialTracks[(index - 1)].gameObject.SetActive(false);

        tutorialTracks[index].gameObject.SetActive(true);
    }

    public void ReturnToTrackPool()
    {
        foreach (GameObject track in trackPool)
        {
            track.SetActive(false);
        }
    }

    public void DestroyTrackPool()
    {
        trackPool.Clear();

        Destroy(trackParent);

        trackParent = null;
    }

    public void DestroyTutorialTrack()
    {
        tutorialTracks.Clear();

        Destroy(tutorialTrackParent);
    }
}
