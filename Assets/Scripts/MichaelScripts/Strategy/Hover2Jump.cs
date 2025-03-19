//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover2Jump : IJumpingStrategy
{
    //private GameObject player;
    private Rigidbody2D playerRB;

    public void SecondJump(Rigidbody2D playerRB)
    {
        //player = GameObject.Find("CatPlayer");
        //playerRB = player.GetComponent<Rigidbody2D>();
        Debug.Log("Strategy Pattern Hover");

        playerRB.gravityScale = 0;
    }

    public int SecondJumpMaximum()
    {
        return 1;
    }
}
