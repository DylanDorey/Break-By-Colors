using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/15/2024]
 * [A track that spawns and moves beneath the player controller]
 */

public class Track : MonoBehaviour
{
    //speed of the track
    private float speed;
    private int localTrackSize;
    private int trackLength;
    private Vector3 spawnPos;

    public Transform[] wallSpawnPoints;
    public List<Wall> walls;
    public GameObject wallPrefab;

    public int wallSpawnChance;

    private void FixedUpdate()
    {
        if (TrackSpawner.Instance.moving)
        {
            Move();
        }
    }

    /// <summary>
    /// Allows the track to move in a continous direction
    /// </summary>
    private void Move()
    {
        //if the track moves too far behind the player
        if (transform.position.z < -localTrackSize)
        {
            //go back to the start of the track length
            transform.position = spawnPos;
            OnTrackReset();
        }

        //move along the z axis towards the player at a particular speed
        transform.Translate(0f, 0f, (-speed * Time.deltaTime));
    }

    public float SetSpeed(float newSpeed)
    {
        speed = newSpeed;

        return speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// Initializes the track section
    /// </summary>
    /// <param name="trackSize"></param>
    /// <param name="totalTrackLength"></param>
    /// <param name="trackSpeed"></param>
    public void InitializeTrack(int trackSize, int totalTrackLength, float startSpeed, int spawnChance)
    {
        localTrackSize = trackSize;
        trackLength = totalTrackLength;
        speed = startSpeed;
        wallSpawnChance = spawnChance;

        spawnPos = new Vector3(0f, -0.7f, (localTrackSize * trackLength) - (localTrackSize));
        InitializeWalls();
    }

    /// <summary>
    /// 
    /// </summary>
    private void InitializeWalls()
    {
        GameObject wallObjectParent = new GameObject("WallObjectParent");
        wallObjectParent.transform.parent = gameObject.transform;

        for (int index = 0; index < wallSpawnPoints.Length; index++)
        {
            GameObject wall = Instantiate(wallPrefab, wallSpawnPoints[index].position, Quaternion.identity);
            walls.Add(wall.GetComponent<Wall>());

            wall.transform.parent = wallObjectParent.transform;

            wall.name = "Wall";
        }

        int randomWallVisibility = Random.Range(0, 2);

        if (randomWallVisibility == 1)
        {
            wallObjectParent.SetActive(true);
        }
        else
        {
            wallObjectParent.SetActive(false);
        }

        int randomGapIndex = Random.Range(0, 6);

        for (int index = 0; index < walls.Count; index++)
        {
            if (index == randomGapIndex)
            {
                walls[index].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnTrackReset()
    {
        int randomGapIndex = Random.Range(0, 6);

        for (int index = 0; index < walls.Count; index++)
        {
            walls[index].gameObject.SetActive(true);

            if (index == randomGapIndex)
            {
                walls[index].gameObject.SetActive(false);
            }
        }
    }
}
