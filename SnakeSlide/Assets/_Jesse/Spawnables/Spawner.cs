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

            switch (currentDifficulty)
            {
                case DIFFICULTY.EASY:
                    if (levelType == TYPE.A)
                    {
                        var spawnVector = new Vector3(PrefabEasyA.transform.position.x,
                            transform.position.y + PrefabEasyA.transform.position.y);
                        Instantiate(PrefabEasyA, spawnVector, Quaternion.identity);

                    } else if (levelType == TYPE.B)
                    {
                        var spawnVector = new Vector3(PrefabEasyB.transform.position.x,
                            transform.position.y + PrefabEasyA.transform.position.y);
                        Instantiate(PrefabEasyB, spawnVector, Quaternion.identity);

                    } else if (levelType == TYPE.C && levelType != TYPE.B || levelType != TYPE.A)
                    {
                        var spawnVector = new Vector3(PrefabNormalA.transform.position.x,
                            PrefabNormalA.transform.position.y + transform.position.y);
                        Instantiate(PrefabNormalA, spawnVector, Quaternion.identity);
                    }

                    break;

                case DIFFICULTY.NORMAL:
                    if (levelType == TYPE.A)
                    {
                        var spawnVector = new Vector3(PrefabNormalA.transform.position.x,
                            transform.position.y + PrefabNormalA.transform.position.y);
                        Instantiate(PrefabNormalA, spawnVector, Quaternion.identity);

                    }
                    else if (levelType == TYPE.B)
                    {
                        var spawnVector = new Vector3(PrefabNormalB.transform.position.x,
                            transform.position.y + PrefabNormalB.transform.position.y);
                        Instantiate(PrefabNormalB, spawnVector, Quaternion.identity);

                    }
                    else if (levelType == TYPE.C && levelType != TYPE.B || levelType != TYPE.A)
                    {
                        var spawnVector = new Vector3(PrefabNormalC.transform.position.x,
                            PrefabNormalC.transform.position.y + transform.position.y);
                        Instantiate(PrefabNormalC, spawnVector, Quaternion.identity);
                    }

                    break;

                case DIFFICULTY.HARD:
                    if (levelType == TYPE.A)
                    {
                        var spawnVector = new Vector3(PrefabHardA.transform.position.x,
                            transform.position.y + PrefabHardA.transform.position.y);
                        Instantiate(PrefabHardA, spawnVector, Quaternion.identity);
                    }
                    else if (levelType == TYPE.B)
                    {
                        var spawnVector = new Vector3(PrefabHardB.transform.position.x,
                            transform.position.y + PrefabHardB.transform.position.y);
                        Instantiate(PrefabHardB, spawnVector, Quaternion.identity);
                    }
                    else if (levelType == TYPE.C && levelType != TYPE.B || levelType != TYPE.A)
                    {
                        var spawnVector = new Vector3(PrefabHardC.transform.position.x,
                            PrefabHardC.transform.position.y + transform.position.y);
                        Instantiate(PrefabHardC, spawnVector, Quaternion.identity);
                    }

                    break;
            }

            MoveSpawnerToNextArea();
        }
    }

    void MoveSpawnerToNextArea()
    {
        transform.position += new Vector3(0, spawnRateSelf, 0);
    }

}
