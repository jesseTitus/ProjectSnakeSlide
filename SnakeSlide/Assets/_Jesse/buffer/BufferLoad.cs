using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferLoad : MonoBehaviour
{

    public GameObject buffer;
    void Awake ()
    {
        if (TransformationBuffer._instance == null)
            Instantiate(buffer);
    }
}
