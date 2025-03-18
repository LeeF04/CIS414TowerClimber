// Written by Lee Fischer
// 3/11/25

//Editted by Michael Anglemier

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

    //Michael Anglemier
    private IJumpingStrategy jumpingStrategy;
    private bool isGrounded = true;
    private bool doubleJumpAvailable = false;
    private bool isFlipped = false;

    //Temporary
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioClip jumpAudioClip;
    SoundPlayer soundPlayer = new SoundPlayer();
    
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();

        jumpingStrategy = new Default2Jump();
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction > 0.0f)
        {
            playerRb.velocity = new Vector2(direction * moveSpeed, playerRb.velocity.y);
            if (isFlipped)
            {
                FlipSprite(isFlipped);
                isFlipped = false;
            }
        }
        else if (direction < 0.0f)
        {
            playerRb.velocity = new Vector2(direction * moveSpeed, playerRb.velocity.y);
            if (!isFlipped)
            {
                FlipSprite(isFlipped);
                isFlipped = true;
            }
        }
        else
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }

        //Michael Anglemier
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                Debug.Log("Grounded Jump");
            }
            else if (doubleJumpAvailable) 
            {
                //Jump();
                jumpingStrategy.DoubleJump();
                doubleJumpAvailable = false;
                Debug.Log("Double Jump");
            }
            else
            {
                Debug.Log("Failed to Jump");
            }
        }
    }





    //Michael Anglemier

    private void Jump()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
        soundPlayer.PlaySoundWithVariance(playerAudioSource, jumpAudioClip);
    }

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
            doubleJumpAvailable = true;
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
