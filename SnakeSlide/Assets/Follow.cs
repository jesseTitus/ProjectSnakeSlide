using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform FollowTransform;
    public bool followHor;
    public bool followVert;
    public bool followZ;

    Vector3 vec;


    void Start()
    {
        vec = transform.position;
        //Debug.Log(string.Format("X {0} Y {1} Z{1}", vec.x, vec.y, vec.z));
    }

    /// <summary>
    /// follow the transform of the object
    /// -followHor: follow the object on the x-axis
    /// -followVert: follow the object on the y-axis
    /// </summary>
    void Update ()
    {
        if (FollowTransform == null)
            return;

        if (followHor)
        {
            vec = new Vector3(FollowTransform.position.x, vec.y, vec.z);
        }

        if (followVert)
        {
            vec = new Vector3(vec.x, FollowTransform.position.y, vec.z);
        }

        if (followZ)
        {
            vec = new Vector3(vec.x, vec.y, FollowTransform.position.z);
        }
       
        transform.position = vec;
    }
}
