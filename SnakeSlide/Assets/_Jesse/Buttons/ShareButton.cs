using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Prompt player to share");
    }
}
