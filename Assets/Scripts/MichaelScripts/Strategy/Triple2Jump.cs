//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triple2Jump : IJumpingStrategy
{
    private GameObject player;
    private Rigidbody2D playerRB;

    private int jumpSpeed = 7;

    public void DoubleJump()
    {
        player = GameObject.Find("CatPlayer");
        playerRB = player.GetComponent<Rigidbody2D>();
        Debug.Log("Strategy Pattern Jump");

        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpSpeed);
    }
}
