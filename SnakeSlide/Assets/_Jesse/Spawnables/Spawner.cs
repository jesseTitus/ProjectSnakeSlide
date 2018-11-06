using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.Presets;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// The Spawner's job is to keep a list of
/// wall objects (WallLeft, WallRight, Center)
/// and spawn them as needed
/// </summary>
public class Spawner : MonoBehaviour
{

    public GameObject wallRPrefab;
    public GameObject wallLPrefab;
    public GameObject wallCPrefab;
    public GameObject pointPrefab;
    public GameObject gemPrefab;


    public float spawnRateSelf = 5f;
    public float spawnRateWallL = 0.75f;
    public float spawnRateWallR = 0.75f;
    public float spawnRateWallC = 0.1f;
    public float spawnRatePoint = 0.4f;
    public float spawnRateGem = 0.1f;
    

    public static Spawner _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Destroy(this);
        }
    }

    void SpawnWallRight()
    {
        var spawnVector = new Vector3(wallRPrefab.transform.position.x,
                                        transform.position.y + wallLPrefab.transform.position.y);
        Instantiate(wallRPrefab, spawnVector, Quaternion.identity);
    }

    void SpawnWallLeft()
    {
        var spawnVector = new Vector3(wallLPrefab.transform.position.x,
                                        transform.position.y + wallLPrefab.transform.position.y);
        Instantiate(wallLPrefab, spawnVector, Quaternion.identity);
    }

    void SpawnWallCenter()
    {
        var spawnVector = new Vector3(wallCPrefab.transform.position.x,
                                        wallCPrefab.transform.position.y + transform.position.y);
        Instantiate(wallCPrefab, spawnVector, Quaternion.identity);
    }

    void SpawnGem()
    {
        var spawnVector = new Vector3(gemPrefab.transform.position.x, 
                                gemPrefab.transform.position.y + transform.position.y);

        Instantiate(gemPrefab, spawnVector, Quaternion.identity);
    }

    void SpawnPoint()
    {
        var spawnVector = new Vector3(pointPrefab.transform.position.x, 
                                        pointPrefab.transform.position.y + transform.position.y);

        Instantiate(pointPrefab, spawnVector, Quaternion.identity);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Procedural generation
            var spawnRightRoll = UnityEngine.Random.Range(0f, 1f) < spawnRateWallR;
            var spawnLeftRoll = UnityEngine.Random.Range(0f, 1f) < spawnRateWallL;
            var spawnCenterRoll = UnityEngine.Random.Range(0f, 1f) < spawnRateWallC;
            var spawnGemRoll = UnityEngine.Random.Range(0f, 1f) < spawnRateGem;
            var spawnPointRoll = UnityEngine.Random.Range(0f, 1f) < spawnRatePoint;
            if (spawnRightRoll)
            {
                SpawnWallRight();
            }

            if (spawnLeftRoll)
            {
                SpawnWallLeft();
            }
            // don't allow left, right AND center (crowded)
            if (spawnCenterRoll && !(spawnLeftRoll || spawnRightRoll))
            {
                SpawnWallCenter();
            }

            if (spawnGemRoll)
            {
                SpawnGem();
            }
            if (spawnPointRoll && !spawnGemRoll)
            {
                SpawnPoint();
            }

            

            MoveSpawnerToNextArea();
        }
    }

    void MoveSpawnerToNextArea()
    {
        transform.position += new Vector3(0, spawnRateSelf, 0);
    }

}
