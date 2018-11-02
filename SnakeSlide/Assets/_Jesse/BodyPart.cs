using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public float distance = 3f;

    Transform followTransform;


    public void Setup(Transform followTransform)
    {
        this.followTransform = followTransform;
    }

    void FollowLeader()
    {
        var diff = transform.position - followTransform.position;
        transform.position = followTransform.position + diff.normalized * distance;
    }
}
