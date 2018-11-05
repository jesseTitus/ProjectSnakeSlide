using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdsButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Prompt player to remove ads");
    }
}
