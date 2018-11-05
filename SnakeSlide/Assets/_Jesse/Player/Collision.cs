using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle all player collision here:
/// - bounds & walls (gameover)
/// - points & gems
/// </summary>
public class Collision : MonoBehaviour
{

    GM gm;
    Player _player;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GM>();
        _player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(string.Format("Player collision: {0}", other.name));

        if (other.GetComponent<Boundary>() || other.GetComponent<Wall>())
        {
            GameObject.Destroy(transform.parent.gameObject);
            gm.Gamestate = GM.GAMESTATE.LOSE;

        } else if (other.GetComponent<Point>())
        {
            _player.AddBodyPart();
            gm.PointsUp();
            Destroy(other.gameObject);
        } else if (other.GetComponent<Gem>())
        {
            gm.GemsUp();
            Destroy(other.gameObject);
        }
    }
}
