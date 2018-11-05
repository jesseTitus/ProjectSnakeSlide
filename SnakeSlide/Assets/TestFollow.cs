using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestFollow : MonoBehaviour
{
    Queue<Transform> followBuffer = new Queue<Transform>(); // keeps track of parent Transform over time

    Transform followTransform;
    Transform oldestTransform;
    bool firstPiece = false;

    [Range(0f, 1f)]
    public float separation;

    [Range(0.01f, 10f)]
    public float positionAlignSpeed;

    [Range(0.01f, 10f)]
    public float rotationAlignSpeed;

    public void Setup(bool firstPiece, Transform followTransform, Queue<Transform> followBuffer)
    {
        foreach (var transform1 in followBuffer)
        {
            Debug.Log("Buffer: " + transform1);
        }
        if (followBuffer.Count == 0)
            Debug.LogError("Empty followBuffer");

        this.firstPiece = firstPiece;
        this.followTransform = followTransform;
        this.followBuffer = followBuffer;
        separation = firstPiece ? separation * 1.4f : separation;

        // Continue tracking transform of parent over time (followBuffer)
        StartCoroutine("IBuffer");
    }

    IEnumerator Follow()
    {
        while (true)
        {

            transform.position = oldestTransform.position;
            transform.rotation = oldestTransform.rotation;

            //transform.position = Vector3.Slerp(transform.position,
            //    oldestTransform.position - (oldestTransform.up * separation), Time.deltaTime * positionAlignSpeed);

            //transform.rotation = Quaternion.Slerp(transform.rotation, oldestTransform.rotation, Time.deltaTime * rotationAlignSpeed);

            yield return null;
        }
    }

    // Store parent transform over time (Circular buffer)
    // Used to follow exact path
    // *may store every nth frame and lerp to be more performant
    IEnumerator IBuffer()
    {
        transform.rotation = followTransform.rotation;
        transform.position = followTransform.position - (followTransform.up * separation);

        oldestTransform = followBuffer.Dequeue();
        followBuffer.Enqueue(followTransform);
        StartCoroutine("Follow");

        while (true)
        {
            oldestTransform = followBuffer.Dequeue();
            followBuffer.Enqueue(followTransform);
            yield return null;
            //yield return new WaitForSeconds(1 * Time.deltaTime);   // x * deltaTime = num frames?
        }
    }
}
