using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour {

    public enum BUTTON_ACTIONS
    {
        LIKE,
        LEADERBOARD,
        TROPHY,
        SHARE,
        RESTART,
        BUY_GEMS,
        SOUND,
        SHOP,
        REMOVE_ADS,
        WATCH_AD
    }

    public BUTTON_ACTIONS ActionImplemented { get; set; }

    public void Activate()
    {
        FindObjectOfType<GM>().ButtonActivated(ActionImplemented);
    }
}
