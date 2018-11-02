using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    #region STATES
    public enum GAMESTATE
    {
        GAMEOVER,
        RESTART,
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
        Gamestate = GAMESTATE.RESTART;
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

    public void ButtonActivated(ButtonAction.BUTTON_ACTIONS action)
    {
        switch (action)
        {
            case ButtonAction.BUTTON_ACTIONS.LIKE:
                Debug.Log("Like button pressed!");
                break;

            case ButtonAction.BUTTON_ACTIONS.LEADERBOARD:
                Debug.Log("Accessing leaderboards...");
                break;
            case ButtonAction.BUTTON_ACTIONS.TROPHY:
                Debug.Log("Trophy button pressed");
                break;
            case ButtonAction.BUTTON_ACTIONS.SHARE:
                Debug.Log("Prompt player to share");
                break;
            case ButtonAction.BUTTON_ACTIONS.RESTART:
                Gamestate = GAMESTATE.RESTART;
                Debug.Log("Resart");
                break;
            case ButtonAction.BUTTON_ACTIONS.BUY_GEMS:
                Debug.Log("Open up gems microtransaction");
                break;
            case ButtonAction.BUTTON_ACTIONS.SOUND:
                Debug.Log("Toggle sound");
                break;
            case ButtonAction.BUTTON_ACTIONS.SHOP:
                Debug.Log("Open shop");
                break;
            case ButtonAction.BUTTON_ACTIONS.REMOVE_ADS:
                Debug.Log("Prompt player to remove ads");
                break;
            case ButtonAction.BUTTON_ACTIONS.WATCH_AD:
                Debug.Log("Watching ad...");
                break;
        }
    }
}
