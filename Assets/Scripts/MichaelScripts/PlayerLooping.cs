//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooping : MonoBehaviour
{

    private Transform playerPosition;
    private float viewBoundaryX = 11.75f;

    void Start()
    {
        playerPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if (playerPosition != null) 
        {
            if (playerPosition.transform.position.x > viewBoundaryX)
            {
                playerPosition.transform.position = new Vector3(-viewBoundaryX, playerPosition.transform.position.y, playerPosition.transform.position.z);
                //Debug.Log("Player looped from right -> left");
            }
            else if (playerPosition.transform.position.x < -viewBoundaryX)
            {
                playerPosition.transform.position = new Vector3(viewBoundaryX, playerPosition.transform.position.y, playerPosition.transform.position.z);
                //Debug.Log("Player looped from right <- left");
            }

        }
        else
        {
            Debug.LogWarning("playerPosition is NULL in PlayerLoopingScript");
        }

    }
}
