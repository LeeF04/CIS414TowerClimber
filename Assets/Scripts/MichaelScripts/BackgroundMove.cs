//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{

    private Transform backgroundPosition;
    private GameObject player;
    private int backgroundParallax = 60;
    private float backgroundStartY = 3.5f;


    void Start()
    {
        player = GameObject.Find("CatPlayer");
        backgroundPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.y > backgroundStartY)
            {
                backgroundPosition.transform.position = new Vector3(0, (player.transform.position.y - (player.transform.position.y / backgroundParallax)), 0);
            }
        }
        else
        {
            Debug.LogWarning("player is NULL in CameraFollow script");
        }
    }
}
