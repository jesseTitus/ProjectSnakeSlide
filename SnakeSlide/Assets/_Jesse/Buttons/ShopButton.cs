using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Open shop");
    }
}
