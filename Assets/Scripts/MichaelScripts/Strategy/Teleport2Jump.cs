//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport2Jump : IJumpingStrategy
{
    private GameObject player;
    private Transform playerPosition;

    public void DoubleJump()
    {
        player = GameObject.Find("CatPlayer");
        playerPosition = player.GetComponent<Transform>();
        Debug.Log("Strategy Pattern Teleport");

        playerPosition.transform.position = new Vector3(-playerPosition.transform.position.x, playerPosition.transform.position.y+5, playerPosition.transform.position.z);
    }
}
