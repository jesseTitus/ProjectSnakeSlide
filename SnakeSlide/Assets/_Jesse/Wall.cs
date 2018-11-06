using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Wall : Destroyable {

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.GetComponent<Gem>() || other.GetComponent<Point>())
    //    Destroy(other.gameObject);
    //}


    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
