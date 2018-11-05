using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool debugMode;

    [Tooltip("Use this to track number of frames tracked and passed to new bodyparts")]
    public int framesTracked;
    
    Queue<Transform> followBuffer = new Queue<Transform>(); // keeps track of parent Transform over time
    public List<Vector3> debugBuffer;

    public List<Transform> BodyParts = new List<Transform>();
    public int beginSize;
    public float acceleration;
    public float maxSpeed = 6f; //speed player reaches after a couple seconds at start
    public float curSpd = 0f;   // this speed is what actually controls the player, it slowly increases * acceleration
    public float rotSpd = 3f;   //feels close to real game

    float maxAngleR = -45f;
    float maxAngleL = 45f;

    public GameObject bodyPrefab;

    float distance;
    Transform curBodyPart;
    Transform prevBodyPart;
    int maxBodyParts = 15;


    void Start () {

        for (var i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }

        StartCoroutine("RevEngine");
        StartCoroutine("IPassedBuffer");
    }
    
    void Update () {
        Move();

        if (Input.GetMouseButtonDown(1)) 
            AddBodyPart();
    }

    public void Move()
    {
        // Circular buffer (track transform)


        float goal = maxAngleR;
        // snake head always moves right (45), unless input (-45)
        if (Input.GetMouseButton(0))
            goal = maxAngleL;
        
        if (debugMode)
            goal = 0;


        Quaternion newR = Quaternion.AngleAxis(goal, Vector3.forward);
        BodyParts[0].rotation = Quaternion.Slerp(BodyParts[0].transform.rotation, 
                                                    newR, 
                                                    rotSpd * Time.deltaTime);


        BodyParts[0].position += BodyParts[0].up * curSpd * Time.deltaTime;

        //for (int i = 1; i < BodyParts.Count; i++)
        //{
        //    curBodyPart = BodyParts[i];
        //    prevBodyPart = BodyParts[i - 1];
        //    distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

        //    Vector3 newPos = prevBodyPart.position;

        //    newPos.y = BodyParts[0].position.y;

        //    float T = Time.deltaTime * distance / minDistance * curSpd; //far away will move faster

        //    if (T > 0.5f)
        //        T = 0.5f;

        //    curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
        //    curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        //}
    }

    public void AddBodyPart()
    {
        if (BodyParts.Count > maxBodyParts)           // don't add parts beyond what player can see!
        {
            return;
        }


        Transform newPart = (Instantiate(
                                bodyPrefab, 
                                BodyParts[BodyParts.Count-1].position,
                                BodyParts[BodyParts.Count - 1].rotation) 
                                as GameObject).transform;

        var partClass = newPart.GetComponent<TestFollow>();
        //partClass.followTransform = BodyParts[BodyParts.Count - 1];

        var isFirstBodyPart = BodyParts.Count == 1;
        partClass.Setup(isFirstBodyPart, BodyParts[BodyParts.Count -1], followBuffer);

        newPart.SetParent(transform);
        BodyParts.Add(newPart);

        // End tracking since no new parts to be added
        if (BodyParts.Count > maxBodyParts)
        {
            StopCoroutine("IPassedBuffer");
        }
    }

    IEnumerator RevEngine()
    {
        while (curSpd < maxSpeed)
        {
            curSpd += acceleration * Time.deltaTime;
            yield return null;
        }

        curSpd = maxSpeed;
        //Debug.Log("Player speed reached peak... ending coroutine RevEngine");
        StopCoroutine("RevEngine");
    }

    // Store a circular buffer of the last bodypart (or head if start of game)
    // and pass on that buffer to newly added bodyparts
    IEnumerator IPassedBuffer()
    {
        // Get initial buffer filled
        while (followBuffer.Count < framesTracked)
        {
            followBuffer.Enqueue(BodyParts[BodyParts.Count - 1]);
            debugBuffer = followBuffer.Select(v => v.position).ToList();
            yield return new WaitForSeconds(0.1f);

        }
        Debug.Log("buffer filled");
        // Maintain buffer
        while (true)
        {
            followBuffer.Dequeue();
            followBuffer.Enqueue(BodyParts[BodyParts.Count - 1]);
            debugBuffer = followBuffer.Select(v => v.position).ToList();

            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
