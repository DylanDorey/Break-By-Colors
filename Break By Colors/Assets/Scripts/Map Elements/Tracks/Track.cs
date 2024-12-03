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
    public float speed;
    private int localTrackSize;

    private Material[] wallMaterials = new Material[4];
    public Material[] pullMaterials;

    public Transform[] wallSpawnPoints;
    public List<GameObject> walls;
    public GameObject wallPrefab;
    public GameObject gapCollider;

    public GameObject nextTrack;

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
        if (transform.position.z < -localTrackSize - 10f)
        {
            //go back to the start of the track length
            OnTrackReset();
            /////////////////////////////////////////////////////////////////
            transform.position = SetSpawnPoint();
            //OnTrackReset();
        }

        //move along the z axis towards the player at a particular speed
        transform.Translate(0f, 0f, (-speed * Time.deltaTime));
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
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
    public void InitializeTrack(int trackSize, float startSpeed, int spawnChance)
    {
        localTrackSize = trackSize;
        speed = startSpeed;
        wallSpawnChance = spawnChance;

        InitializeWalls();
    }

    /// <summary>
    /// 
    /// </summary>
    private void InitializeWalls()
    {
        GameObject wallObjectParent = new GameObject("WallObjectParent");
        wallObjectParent.transform.parent = gameObject.transform;

        int randomGapIndex = Random.Range(0, 6);

        for (int index = 0; index < wallSpawnPoints.Length; index++)
        {
            if (index != randomGapIndex)
            {
                GameObject wall = Instantiate(wallPrefab, wallSpawnPoints[index].position, Quaternion.identity);
                walls.Add(wall);

                wall.transform.parent = wallObjectParent.transform;

                wall.name = "Wall";

                //HERE
                //wall.GetComponent<Wall>().SetWallColor(SetMaterial());
            }
            else
            {
                GameObject gap = Instantiate(gapCollider, wallSpawnPoints[index].position, Quaternion.identity);
                walls.Add(gap);

                gap.transform.parent = wallObjectParent.transform;

                gap.name = "Gap";
            }
        }

        InitializeWallColors();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnTrackReset()
    {
        for (int index = 0; index < walls.Count; index++)
        {
            if (walls[index].GetComponent<Gap>())
            {
                GameObject tempWall = walls[index].gameObject;
                walls.RemoveAt(index);

                GameObject wall = Instantiate(wallPrefab, gameObject.transform.GetChild(3).transform.GetChild(index).position, Quaternion.identity);
                walls.Add(wall);
                wall.transform.parent = gameObject.transform.GetChild(3).transform;
                wall.name = "Wall";

                //HERE
                //wall.GetComponent<Wall>().SetWallColor(SetMaterial());

                Destroy(tempWall);
            }
        }

        int randomGapIndex = Random.Range(0, 6);

        Vector3 spawnPoint = walls[randomGapIndex].gameObject.transform.position;

        Destroy(walls[randomGapIndex].gameObject);
        walls.RemoveAt(randomGapIndex);


        GameObject gap = Instantiate(gapCollider, spawnPoint, Quaternion.identity);
        walls.Add(gap);

        gap.transform.parent = gameObject.transform.GetChild(3).transform;

        gap.name = "Gap";

        InitializeWallColors();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Material SetMaterials(int index)
    {
        Material newMaterial = wallMaterials[index];

        return newMaterial;
    }

    private void InitializeWallColors()
    {
        List<int> availablePositions = new List<int> { 0, 1, 2, 3 };

        for (int index1 = 0; index1 < 4; index1++)
        {
            int randomIndex = availablePositions[Random.Range(0, availablePositions.Count)];

            wallMaterials[randomIndex] = pullMaterials[index1];

            availablePositions.Remove(randomIndex);
        }

        int index = 0;
        foreach (GameObject wall in walls)
        {
            if (wall.GetComponent<Wall>())
            {
                if (index < 4)
                {
                    wall.GetComponent<Wall>().SetWallColor(SetMaterials(index));
                    index++;
                }
                else
                {
                    wall.GetComponent<Wall>().SetWallColor(SetMaterials(Random.Range(0, 4)));
                }
            }
        }
    }

    /// <summary>
    /// Sets the spawn point of the current track to the next track in the track pool list
    /// </summary>
    /// <returns></returns>
    private Vector3 SetSpawnPoint()
    {
        return GetComponent<Track>().nextTrack.transform.GetChild(1).transform.position;
    }
}
