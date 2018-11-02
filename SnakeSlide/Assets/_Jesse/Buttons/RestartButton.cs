using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : ButtonAction {
    void Start()
    {
        ActionImplemented = BUTTON_ACTIONS.RESTART;
    }
}
