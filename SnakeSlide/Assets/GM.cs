using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {

    public enum GAMESTATE
    {
        MENU,
        PLAYING,
        LOSE
    }

    GAMESTATE gamestate = GAMESTATE.PLAYING;

    public GAMESTATE Gamestate
    {
        get { return gamestate; }
        set
        {
            gamestate = value;
            Debug.Log(string.Format("Gamestate: {0}", value));
        }
    }
}
