using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour, IButtonAction
{
    Text textDisplayed;
    string[] messages = new string[2] {"Sound ON", "Sound OFF"};

    void Start()
    {
        textDisplayed = GetComponentInChildren<Text>();
        UpdateButtonDisplay();
    }

    public void Execute()
    {
        SoundManager._instance.ToggleAudio();
        UpdateButtonDisplay();
    }

    void UpdateButtonDisplay()
    {
        if (SoundManager._instance.audioPreferences)
        {
            textDisplayed.text = messages[0];
        }
        else
        {
            textDisplayed.text = messages[1];
        }
    }
}
