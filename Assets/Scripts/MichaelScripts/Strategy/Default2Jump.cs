//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default2Jump : IJumpingStrategy
{
    //private GameObject player;
    //private Rigidbody2D playerRB;

    private int jumpSpeed = 10;

    public void SecondJump(Rigidbody2D playerRB)
    {
        //Attempted to debug and find where my NullReference was

        /*
        if (playerRB == null)
        {
            Debug.Log("Strat playerRB == null");
        }
        if (playerRB != null)
        {
            Debug.Log("Strat playerRB != null");
        }
        */

        //player = GameObject.Find("CatPlayer");
        //playerRB = player.GetComponent<Rigidbody2D>();
        Debug.Log("Strategy Pattern Jump");

        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpSpeed);
    }

    public int SecondJumpMaximum()
    {
        return 1;
    }

}
