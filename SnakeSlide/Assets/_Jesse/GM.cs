using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    #region STATES
    public enum GAMESTATE
    {
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
                case GAMESTATE.LOSE:
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
                    Instantiate(player);
                    break;

            }

        }
    }

    #endregion

    public GameObject player;

    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;

    Spawner spawner;

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
        Gamestate = GAMESTATE.PLAY;
        spawner = FindObjectOfType<Spawner>();
    }


    #region scoreHandling
    public void PointsUp()
    {
        if (++points > highScore)
            highScore = points;

        scoreText.text = points.ToString();
    }

    public void GemsUp()
    {
        gems++;
    }

    public void GemsUp(int amt) // For microtransactions
    {
        gems += amt;
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
}
