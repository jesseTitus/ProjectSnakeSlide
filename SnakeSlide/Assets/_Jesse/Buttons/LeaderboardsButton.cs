using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Accessing leaderboards...");
    }
}
