using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation
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

    public Transformation GetDelta(Transformation other)
    {
        Quaternion rot = this.rotation;
        Vector3 pos = this.position - other.position;
        Vector3 up = this.up - other.up;
        return new Transformation(rot, pos, up);
    }
}
