using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour, IButtonAction
{
    public void Execute()
    {
        Debug.Log("Toggle sound");
    }
}
