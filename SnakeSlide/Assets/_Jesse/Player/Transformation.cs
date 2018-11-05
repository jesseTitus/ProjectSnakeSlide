using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Transformation
{
    public Quaternion rotation;
    public Vector3 position;
    public Vector3 up;

    public Transformation(Quaternion rotation,  Vector3 position, Vector3 up)
    {
        this.rotation = rotation;
        this.position = position;
        this.up = up;
    }
}
