using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    /// <summary>
    /// When an object enters the bottom boundary of the screen
    /// and it is of type Destroyable
    /// it will be destroyed
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Destroyable>())
        Destroy(other.gameObject);
    }
}
