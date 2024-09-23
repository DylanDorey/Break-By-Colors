using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/15/2024]
 * [A track spawner that spawns track game objects from the track object pool]
 */

public class TrackSpawner : Singleton<TrackSpawner>
{
    [Range(0f, 50f)] public float startSpeed;
    [Range(1, 30)] public int trackLength;

    [Tooltip("! MUST MATCH THE LENGTH OF THE TRACK PREFAB !")]
    public int trackSize;

    [Range(3, 10)] public int wallSpawnChance;

    public bool startMoving = false;

    //reference to the drone object pool
    private TrackObjectPool pool;

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        //initialize the track object pool
        if (gameObject.GetComponent<TrackObjectPool>())
        {
            pool = gameObject.GetComponent<TrackObjectPool>();
            pool.Spawn();
        }
        else
        {
            Debug.LogError("ERROR: No track object pool found!");
        }
    }

    private void StopSpawning()
    {
        pool.DestroyTrackPool();
    }

    public bool StartMoving()
    {
        startMoving = true;

        return startMoving;
    }
}
