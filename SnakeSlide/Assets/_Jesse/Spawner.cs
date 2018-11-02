using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Presets;
using UnityEngine;

/// <summary>
/// The Spawner's job is to keep a list of
/// wall objects (WallLeft, WallRight, Center)
/// and spawn them as needed
/// </summary>
public class Spawner : MonoBehaviour
{

    public GameObject wallRPrefab;
    public GameObject wallLPrefab;
    public GameObject pointPrefab;
    public GameObject gemPrefab;


    public float spawnRate = 5f;
    public float wallOffsetR = 0f;
    public float wallOffsetL = 0f;
    public float pointOffset = 0f;
    public float gemOffset = 0f;

    List<GameObject> wallsList;

    void Start()
    {
        wallsList = new List<GameObject>();
    }

    void SpawnRight()
    {
        MoveCollider();

        var spawnVector = new Vector3(wallRPrefab.transform.position.x, 
                                        transform.position.y + wallOffsetR);
        var nextWall = Instantiate(wallRPrefab, spawnVector, Quaternion.identity) as GameObject;
        wallsList.Add(nextWall);
    }

    void SpawnLeft()
    {
        MoveCollider();

        var spawnVector = new Vector3(wallLPrefab.transform.position.x, 
                                        transform.position.y + wallOffsetL);
        var nextWall = Instantiate(wallLPrefab, spawnVector, Quaternion.identity) as GameObject;
        wallsList.Add(nextWall);
    }

    void SpawnGem()
    {
        var spawnVector = new Vector3(0, transform.position.y);
        var nextGem = Instantiate(gemPrefab, spawnVector, Quaternion.identity) as GameObject;
    }

    void SpawnPoint()
    {
        var spawnVector = new Vector3(0, transform.position.y - pointOffset);
        var nextPoint = Instantiate(pointPrefab, spawnVector, Quaternion.identity) as GameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnRight();
            SpawnLeft();
            SpawnGem();
            SpawnPoint();
        }
    }

    void MoveCollider()
    {
        transform.position += new Vector3(0, spawnRate, 0);
    }
    
}
