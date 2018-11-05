using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour, IButtonAction
{
    GM gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GM>();
    }
    public void Execute()
    {
        Debug.Log("Resart");
        gameManager.Restart();
    }
}
