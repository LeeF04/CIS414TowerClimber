// Written by Lee Fischer
// 3/11/25

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Variables 
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 10.0f;
    private float direction = 0.0f;

    private Rigidbody2D playerRb;

    
    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if(direction > 0.0f)
        {
            playerRb.velocity = new Vector2(direction * moveSpeed, playerRb.velocity.y);
        }
        else if(direction < 0.0f)
        {
            playerRb.velocity = new Vector2(direction * moveSpeed, playerRb.velocity.y);
        }
        else
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
        }
    }
}
