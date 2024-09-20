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
    public GameObject trackPrefab;
    private GameObject trackParent;

    public List<GameObject> trackPool;

    private void Start()
    {
        maxPoolSize = TrackSpawner.Instance.trackLength;
    }

    public void PopulateTrackPool()
    {
        //create new parent track pool game object
        trackParent = new GameObject("TrackObjectPool");

        //if the max pool size is not initialized
        if(maxPoolSize <= 0)
        {
            //initialize it to the track length
            maxPoolSize = TrackSpawner.Instance.trackLength;
        }

        for (int index = 0; index < maxPoolSize; index++)
        { 
            GameObject track = Instantiate(trackPrefab, Vector3.zero, Quaternion.identity);
            trackPool.Add(track);

            track.transform.parent = trackParent.transform;
            track.GetComponent<Track>().InitializeTrack(TrackSpawner.Instance.trackSize, TrackSpawner.Instance.trackLength, TrackSpawner.Instance.startSpeed);
            track.name = "Track" + index;
        }
    }

    public void Spawn()
    {
        if(trackParent == null)
        {
            PopulateTrackPool();
        }

        float spawnPoint = (TrackSpawner.Instance.trackSize * TrackSpawner.Instance.trackLength) - (TrackSpawner.Instance.trackSize + 20);

        for (int index = 0; index < maxPoolSize; index++)
        {
            GameObject track = trackPool[index];

            track.SetActive(true);

            track.transform.position = new Vector3(0f, -0.7f, spawnPoint);
            spawnPoint -= TrackSpawner.Instance.trackSize;
        }
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
        foreach (GameObject track in trackPool)
        {
            trackPool.Remove(track);
            Destroy(track);
        }

        Destroy(trackParent);
    }
}
