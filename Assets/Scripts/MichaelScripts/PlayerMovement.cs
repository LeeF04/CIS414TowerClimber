// Rewritten from "PlayerMove" by Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables 
    [SerializeField] private float moveSpeed = 5.0f;
    private float direction = 0.0f;

    private Rigidbody2D playerRB;

    //Michael Anglemier
    private bool isGrounded = true;

    //Flipping
    private bool isFlipped = false;

    // Jump Manager
    JumpManager jumpManager = new JumpManager();
    private int AirJumpType;


    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction > 0.0f)
        {
            playerRB.velocity = new Vector2(direction * moveSpeed, playerRB.velocity.y);
            if (isFlipped)
            {
                FlipSprite(isFlipped);
                isFlipped = false;
            }
        }
        else if (direction < 0.0f)
        {
            playerRB.velocity = new Vector2(direction * moveSpeed, playerRB.velocity.y);
            if (!isFlipped)
            {
                FlipSprite(isFlipped);
                isFlipped = true;
            }
        }
        else
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }

        //Michael Anglemier
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpManager.GroundedJump(playerRB);
            }
            else
            {
                //Attempted to debug and find where my NullReference was

                /*
                if (playerRB == null)
                {
                    Debug.Log("Movement playerRB == null");
                }
                if (playerRB != null)
                {
                    Debug.Log("Movement playerRB != null");
                    Debug.Log(playerRB);
                }
                */

                jumpManager.AirJump(AirJumpType, playerRB);
            }

        }

        if (Input.GetKeyDown("1"))
        {
            AirJumpType = 1;
        }

        if (Input.GetKeyDown("2"))
        {
            AirJumpType = 2;
        }

        if (Input.GetKeyDown("3"))
        {
            AirJumpType = 3;
        }

        if (Input.GetKeyDown("4"))
        {
            AirJumpType = 4;
        }

    }


    //Michael Anglemier

    private void FlipSprite(bool isFlipped)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            Debug.Log("Enter Platform");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            Debug.Log("Exit Platform");
        }
    }
}
