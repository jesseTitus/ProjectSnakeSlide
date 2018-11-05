using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    const int MAX_BODY_PARTS = 15;
    const float maxAngleR = -45f;
    const float maxAngleL = 45f;

    public bool debugMode;
    
    
    public List<Transform> BodyParts = new List<Transform>();
    public int beginSize;
    public float acceleration;
    public float maxSpeed = 6f; //speed player reaches after a couple seconds at start
    public float curSpd = 0f;   // this speed is what actually controls the player, it slowly increases * acceleration
    public float rotSpd = 3f;   //feels close to real game



    public GameObject bodyPrefab;
    
    
    void Start () {

        for (var i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }

        StartCoroutine("RevEngine");
        StartCoroutine("FillBuffer");
    }
    
    void Update () {
        Move();

        if (Input.GetMouseButtonDown(1)) 
            AddBodyPart();
    }

    public void Move()
    {
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
    }

    public void AddBodyPart()
    {
        if (BodyParts.Count > MAX_BODY_PARTS)           // don't add parts beyond what player can see!
        {
            return;
        }
        
        Transform newPart = (Instantiate(bodyPrefab, BodyParts[BodyParts.Count-1].position, 
                                            BodyParts[BodyParts.Count - 1].rotation) 
                                            as GameObject).transform;

        var partClass = newPart.GetComponent<TestFollow>();
        
        Transformation tranformation = new Transformation(BodyParts[BodyParts.Count - 1].rotation,
                                                            BodyParts[BodyParts.Count - 1].position,
                                                            BodyParts[BodyParts.Count - 1].up);

        partClass.Setup(BodyParts.Count -1, tranformation);

        newPart.SetParent(transform);
        BodyParts.Add(newPart);
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


    // Store a circular buffer of Transformations (pos/rot) that creat a path of waypoints
    IEnumerator FillBuffer()
    {
        var head = BodyParts[0];
        if (TransformationBuffer._instance == null) 
            Debug.Log("buffer missing");
        while (true)
        {
            Transformation t = new Transformation(head.rotation, head.position, head.up);
            TransformationBuffer._instance.AddTransformation(t);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
