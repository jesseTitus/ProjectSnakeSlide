using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool debugMode;


    public List<Transform> BodyParts = new List<Transform>();
    public float minDistance = 0.25f;   // bodyparts don't collide!
    public int beginSize;
    public float spd = 1f;
    public float curSpd;
    public float rotSpd = 3f;

    float maxAngleR = -45f;
    float maxAngleL = 45f;

    public GameObject bodyPrefab;

    float distance;
    Transform curBodyPart;
    Transform prevBodyPart;


    void Start () {

        for (var i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }
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
        
        //curSpd = Mathf.Lerp(rotSpd, goal, Time.deltaTime);
        
        //Debug.Log(string.Format("GOAL {0} | R: {1}", goal, curSpd));
        if (debugMode)
            goal = 0;


        Quaternion newR = Quaternion.AngleAxis(goal, Vector3.forward);
        BodyParts[0].rotation = Quaternion.Slerp(BodyParts[0].transform.rotation, 
                                                    newR, 
                                                    spd * Time.deltaTime);


        // smoothDeltaTime to smooth out movement
        //BodyParts[0].Translate(BodyParts[0].forward * curSpd * Time.smoothDeltaTime, Space.World);
        //BodyParts[0].position = Vector3.Slerp(BodyParts[0].position,
        //                                        BodyParts[0].position + 
        //                                        BodyParts[0].transform.forward,
        //                                        spd * Time.deltaTime);

        //BodyParts[0].position = Vector3.MoveTowards(transform.position,
        //    transform.up, spd * Time.deltaTime);
        BodyParts[0].position += BodyParts[0].up * spd * Time.deltaTime;

        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];
            distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * distance / minDistance * spd; //far away will move faster

            if (T > 0.5f)
                T = 0.5f;

            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        Transform newPart = (Instantiate(
                                bodyPrefab, 
                                BodyParts[BodyParts.Count-1].position,
                                BodyParts[BodyParts.Count - 1].rotation) 
                                as GameObject).transform;

        newPart.SetParent(transform);
        BodyParts.Add(newPart);
    }
}
