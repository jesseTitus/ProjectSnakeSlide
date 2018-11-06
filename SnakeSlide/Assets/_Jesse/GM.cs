using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    #region STATES
    public enum GAMESTATE
    {
        LOAD,
        GAMEOVER,
        RESTART,
        PLAY,
        LOSE
    }

    GAMESTATE _gamestate;

    public GAMESTATE Gamestate
    {
        get { return _gamestate; }
        set
        {
            _gamestate = value;
            Debug.Log(string.Format("Gamestate: {0}", value));
            switch (value)
            {
                case GAMESTATE.LOAD:
                    Load();
                    Gamestate = GAMESTATE.PLAY;
                    break;

                case GAMESTATE.LOSE:
                    SoundManager._instance.PlayMusic(false);
                    SoundManager._instance.PlayDieSound();
                    Gamestate = GAMESTATE.GAMEOVER;
                    break;

                case GAMESTATE.GAMEOVER:
                    DisplayGameOver();
                    break;

                case GAMESTATE.RESTART:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;

                case GAMESTATE.PLAY:
                    DisplayRestart();
                    SoundManager._instance.PlayMusic(true);
                    break;

            }

        }
    }

    #endregion

    public static GM _instance;

    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;

    public Text scoreText;
    public Text scoreEndText;
    public Text highScoreText;
    public Text gemsEndText;

    int highScore = 0;
    int points = 0;
    int gems = 0;


    //-------
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

    void Start()
    {
        Gamestate = GAMESTATE.LOAD;
    }


    #region scoreHandling
    public void PointsUp()
    {
        SoundManager._instance.PlayPointSound();
        if (++points > highScore)
        {
            highScore = points;
            Save();
        }

        scoreText.text = points.ToString();
    }

    public void GemsUp()
    {
        SoundManager._instance.PlayGemSound();
        gems++;
        Save();
    }

    public void GemsUp(int amt) // For microtransactions
    {
        gems += amt;
        Save();
    }
    #endregion
    
    

    public void DisplayGameOver()
    {
        InGameCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
        scoreEndText.text = points.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        gemsEndText.text = "GEMS: " + gems.ToString();
    }

    public void DisplayRestart()
    {
        GameOverCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
    }

    public void Restart()
    {
        Gamestate = GAMESTATE.RESTART;
        
    }

    #region SAVE/LOAD
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData(highScore, gems, SoundManager._instance.audioPreferences);
        
        // take our playerdata and save it to "file"
        bf.Serialize(file, data);   
        file.Close();
    }

    public void Load()
    {
        // get highscore, sound preference, gems
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Save();
        }

        //Debug.Log(Application.persistentDataPath + "/playerInfo.dat");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();

        // Setup data
        highScore = data.highscore;
        gems = data.gems;
        SoundManager._instance.audioPreferences = data.audioPreferences;
    }
    #endregion
}

[Serializable]  // serializable: can now save this to a file (no monobehavior's allowed!)
class PlayerData
{
    public int highscore;
    public int gems;
    public bool audioPreferences;

    public PlayerData(int highscore, int gems, bool audioPreference)
    {
        this.highscore = highscore;
        this.gems = gems;
        this.audioPreferences = audioPreference;
    }
}
