using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _instance;

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
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Destroy(this);
        }


        for (var i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }

        StartCoroutine("RevEngine");
        //StartCoroutine("FillBuffer");


        if (TransformationBuffer._instance == null)
            Debug.Log("buffer missing");
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

        partClass.Setup(BodyParts.Count -1, tranformation, BodyParts[BodyParts.Count - 1]);

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
    //IEnumerator FillBuffer()
    void LateUpdate()
    {
        //while (true)
        //{
        var head = BodyParts[0];
        Transformation t = new Transformation(head.rotation, head.position, head.up);
        TransformationBuffer._instance.AddTransformation(t);

        //yield return new WaitForSeconds(1 * Time.deltaTime);
        //}
    }
}
