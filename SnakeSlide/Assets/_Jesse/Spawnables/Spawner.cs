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
    public enum DIFFICULTY
    {
        EASY,
        NORMAL,
        HARD
    }

    public enum TYPE
    {
        EMPTY,
        A,
        B,
        C,
    }

    public GameObject PrefabEasyA;      // no collectables
    public GameObject PrefabEasyB;      //point
    public GameObject PrefabEasyC;      //gem
    public GameObject PrefabNormalA;   
    public GameObject PrefabNormalB;
    public GameObject PrefabNormalC;
    public GameObject PrefabHardA;
    public GameObject PrefabHardB;
    public GameObject PrefabHardC;


    public float spawnRateSelf = 5f;
    public float spawnRateGenA = 0.55f;
    public float spawnRateGenB = 0.25f;
    public float spawnRateGenC = 0.15f;
    public float spawnDifficultyInterval = 10f;    // climbs as player passes this amount
    

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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Procedural generation
            // This section decides what pieces to spawn
            var levelType = UnityEngine.Random.Range(0f, 1f) < spawnRateGenA? TYPE.A:
                UnityEngine.Random.Range(0f, 1f) < spawnRateGenB? TYPE.B:
                UnityEngine.Random.Range(0f, 1f) < spawnRateGenC? TYPE.C:
                TYPE.EMPTY;

            var currentDifficulty = transform.position.y > spawnDifficultyInterval * 3?
                DIFFICULTY.HARD: transform.position.y > spawnDifficultyInterval * 2?
                DIFFICULTY.NORMAL:
                DIFFICULTY.EASY;

            var spawnedPiece = PrefabEasyA;

            switch (currentDifficulty)
            {
                case DIFFICULTY.EASY:
                    if (levelType == TYPE.A)
                    {
                        spawnedPiece = PrefabEasyA;
                    } else if (levelType == TYPE.B)
                    {
                        spawnedPiece = PrefabEasyB;

                    } else if (levelType == TYPE.C)
                    {
                        spawnedPiece = PrefabEasyC;
                    }

                    break;

                case DIFFICULTY.NORMAL:
                    if (levelType == TYPE.A)
                    {
                        spawnedPiece = PrefabNormalA;

                    }
                    else if (levelType == TYPE.B)
                    {
                        spawnedPiece = PrefabNormalB;

                    }
                    else if (levelType == TYPE.C)
                    {
                        spawnedPiece = PrefabNormalC;
                    }

                    break;

                case DIFFICULTY.HARD:
                    if (levelType == TYPE.A)
                    {
                        spawnedPiece = PrefabHardA;
                    }
                    else if (levelType == TYPE.B)
                    {
                        spawnedPiece = PrefabHardB;
                    }
                    else if (levelType == TYPE.C)
                    {
                        spawnedPiece = PrefabHardC;
                    }

                    break;
            }

            var spawnVector = new Vector3(spawnedPiece.transform.position.x,
                transform.position.y + spawnedPiece.transform.position.y);
            Instantiate(spawnedPiece, spawnVector, Quaternion.identity);

            MoveSpawnerToNextArea();
        }
    }

    void MoveSpawnerToNextArea()
    {
        transform.position += new Vector3(0, spawnRateSelf, 0);
    }

}
