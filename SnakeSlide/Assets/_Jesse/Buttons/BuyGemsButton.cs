using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGemsButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Open up gems microtransaction");
    }
}
