//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform cameraPosition;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("CatPlayer");
        cameraPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if (player != null) 
        { 

            if (player.transform.position.y > 0)
            {
                cameraPosition.transform.position = new Vector3(0, player.transform.position.y, -1);
            }
            else
            {
                //Debug.Log("player is below Y 0 coordinate in CameraFollow script");
            }
        }
        else
        {
            Debug.LogWarning("player is NULL in CameraFollow script");
        }
        

    }
}
