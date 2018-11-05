using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestFollow : MonoBehaviour
{
    int index;
    Transformation wayPoint;

    [Range(0, 20)]
    public int framesPerRetrieval;

    [Range(0f, 1f)]
    public float separation;

    [Range(0.01f, 10f)]
    public float positionAlignSpeed;

    [Range(0.01f, 10f)]
    public float rotationAlignSpeed;

    public void Setup(int index, Transformation spawnPosition)
    {
        // Spawn piece
        this.index = index;
        separation = (index == 1) ? separation * 1.4f : separation;
        transform.rotation = spawnPosition.rotation;
        transform.position = spawnPosition.position - (spawnPosition.up * separation);

        // Start following
        StartCoroutine("UpdateWayPoint");
    }

    void Update()
    {
        // 2a. Go directly
        //transform.position = wayPoint.position;
        //transform.rotation = wayPoint.rotation;

        // 2b. Lerp to
        //transform.position = Vector3.Slerp(transform.position,
        //    wayPoint.position -
        //    (wayPoint.up * separation),
        //    Time.deltaTime * positionAlignSpeed);
        transform.position = Vector3.MoveTowards(transform.position, wayPoint.position, separation);

        transform.rotation = Quaternion.Slerp(transform.rotation,
            wayPoint.rotation,
            Time.deltaTime * rotationAlignSpeed);
    }

    IEnumerator UpdateWayPoint()
    {

        while (true)
        {
            // 1. RetrieveTran
            wayPoint = RetrieveTransformation();

            yield return new WaitForSeconds(framesPerRetrieval * Time.deltaTime);   // x * deltaTime = num frames?
        }
    }


    Transformation RetrieveTransformation()
    {
        return TransformationBuffer._instance.GetTransformation(index);
    }
}
