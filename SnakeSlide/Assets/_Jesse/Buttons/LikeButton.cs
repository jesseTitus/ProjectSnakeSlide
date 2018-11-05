using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Like button pressed!");
    }
}
