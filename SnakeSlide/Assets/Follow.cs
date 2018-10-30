using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform PlayerTransform;

    
    
    void Update ()
    {
        transform.position = PlayerTransform.position + new Vector3(0,0,-10);
    }
}
