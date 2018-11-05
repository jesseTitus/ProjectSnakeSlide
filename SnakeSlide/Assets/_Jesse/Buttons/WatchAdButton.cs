using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Watching ad...");
    }
}
