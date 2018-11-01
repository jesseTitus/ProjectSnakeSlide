using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    GM gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GM>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(string.Format("Player collision: {0}", other.name));
        if (other.GetComponent<Boundary>())
        {
            Debug.Log("Collided with boundary - gameover");

            GameObject.Destroy(transform.parent.gameObject);
            gm.Gamestate = GM.GAMESTATE.LOSE;

            //gameObject.SetActive(false);
        }
    }
}
