using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestFollow : MonoBehaviour
{
    int index;
    Transform parent;
    Transformation wayPoint;

    [Range(1, 20)]
    public int framesPerRetrieval;

    [Range(0f, 1f)]
    public float separation;

    [Range(0.01f, 10f)]
    public float positionAlignSpeed;

    [Range(0.01f, 10f)]
    public float rotationAlignSpeed;

    public void Setup(int index, Transformation spawnPosition, Transform parent)
    {
        // Spawn piece
        this.index = index;
        this.parent = parent;
        separation = (index == 0) ? separation * 1.4f : separation;
        transform.rotation = spawnPosition.rotation;
        transform.position = spawnPosition.position - (spawnPosition.up * separation);

        // Start following
        StartCoroutine("UpdateWayPoint");
    }

    void Update()
    {
        //2a.Go directly
        foreach (var t in TransformationBuffer._instance._transformations)
        {
            if (Mathf.Abs(t.position.x - transform.position.x) <= 0.2f && Mathf.Abs(t.position.y - transform.position.y) <= 0.2f)
            {
                transform.position = new Vector3(t.position.x, transform.position.y, transform.position.z);
                transform.rotation = t.rotation;
            }
        }

        transform.Translate(new Vector2(0, 1) * (Time.deltaTime * Player._instance.curSpd));
        //transform.position = wayPoint.position;
        //transform.rotation = wayPoint.rotation;

        // 2b. Lerp to
        //transform.position = Vector3.Lerp(transform.position, wayPoint.position, Time.deltaTime * positionAlignSpeed);
        //transform.position = Vector3.Slerp(transform.position,
        //    wayPoint.position -
        //    (wayPoint.up * separation),
        //    Time.deltaTime * positionAlignSpeed);
        // transform.position = Vector3.MoveTowards(transform.position, wayPoint.position, separation);

        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //    parent.rotation,
        //    Time.deltaTime * rotationAlignSpeed);
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
        var transform1 = TransformationBuffer._instance.GetTransformation(index);
        //var transform2 = TransformationBuffer._instance.GetTransformation(index + 1);
        return transform1;
    }
}
