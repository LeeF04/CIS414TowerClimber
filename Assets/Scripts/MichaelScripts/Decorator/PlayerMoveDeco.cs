// Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMoveDeco : MonoBehaviour
{

    // Moving
    [SerializeField] private float moveSpeed = 5.0f;
    private float direction = 0.0f;

    // Player X Flipping
    private bool isFlipped = false;

    // Rigidbody2D
    private Rigidbody2D playerRB;
    private Transform playerPosition;

    // Gravity Scale
    [SerializeField] private float defaultPlayerGravityScale = 1.5f;

    // Jumping
    private bool isGrounded = true;
    private int AirJumpCount = 0;

    private IMoveState currentMoveState;

    private GameObject lastPlatform; // Bryan Castaneda - Stores the last platform the player landed on this is for score counter

    // Grounded Jump

    [SerializeField] private JumpConfig GroundJumpConfig;

    //[SerializeField] private AudioClip GroundedJumpAudioClip;
    //[SerializeField] private float GroundedJumpSpeed = 10.0f;

    // Double Jump
    [SerializeField] private JumpConfig DoubleJumpConfig;

    // Teleport Jump
    [SerializeField] private JumpConfig TeleportJumpConfig;

    // Triple Jump
    [SerializeField] private JumpConfig TripleJumpConfig;

    // Hover Jump
    [SerializeField] private JumpConfig HoverJumpConfig;

    // Sound
    [SerializeField] AudioSource playerAudioSource;
    SoundPlayer soundPlayer = new SoundPlayer();

    //Animation
    private Animator playerAnimator;

    // Active Jump Configs
    private JumpConfig airJumpConfig = null;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        currentMoveState = new GroundedState();

    }

    void Update()
    {
        #region Walking
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
        #endregion

        #region Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (currentMoveState.StateType() == 1)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, GroundJumpConfig.JumpSpeed);

                soundPlayer.PlaySoundWithVariance(playerAudioSource, GroundJumpConfig.JumpAudio);

                if (GroundJumpConfig.HoverDuration > 0)
                {
                    playerRB.gravityScale = 0;
                    StartCoroutine(ResetGravityToDefault(GroundJumpConfig.HoverDuration));
                }

                if (GroundJumpConfig.Teleport)
                {
                    playerPosition = playerRB.GetComponent<Transform>();
                    playerPosition.transform.position = new Vector3(-playerPosition.transform.position.x, playerPosition.transform.position.y + GroundJumpConfig.JumpSpeed, playerPosition.transform.position.z);
                }
            }
            else if (currentMoveState.StateType() == 2)
            {
                if (airJumpConfig != null)
                { 
                    if (AirJumpCount < airJumpConfig.JumpCountMaximum)
                    {
                        playerRB.velocity = new Vector2(playerRB.velocity.x, airJumpConfig.JumpSpeed);
                        AirJumpCount++;

                        soundPlayer.PlaySoundWithVariance(playerAudioSource, airJumpConfig.JumpAudio);

                        if (airJumpConfig.HoverDuration > 0)
                        {
                            playerRB.gravityScale = 0;
                            StartCoroutine(ResetGravityToDefault(airJumpConfig.HoverDuration));
                        }

                        if (airJumpConfig.Teleport)
                        {
                            playerPosition = playerRB.GetComponent<Transform>();
                            playerPosition.transform.position = new Vector3(-playerPosition.transform.position.x, playerPosition.transform.position.y + airJumpConfig.JumpSpeed, playerPosition.transform.position.z);
                        }
                    }
                }
                else 
                {
                    Debug.Log("airJumpConfig == null");
                }
                
            }
            else
            {
                Debug.Log("Not in the air or on the ground");
            }
        }
        #endregion

        #region GetKetDown

        if (Input.GetKeyDown("0"))
        {
            airJumpConfig = null;
            Debug.Log("Null Jump Selected");
        }

        if (Input.GetKeyDown("1"))
        {
            airJumpConfig = DoubleJumpConfig;
            Debug.Log("Double Jump Selected");
        }

        if (Input.GetKeyDown("2"))
        {
            airJumpConfig = TeleportJumpConfig;
            Debug.Log("Teleport Jump Selected");
        }

        if (Input.GetKeyDown("3"))
        {
            airJumpConfig = TripleJumpConfig;
            Debug.Log("Triple Jump Selected");
        }

        if (Input.GetKeyDown("4"))
        {
            airJumpConfig = HoverJumpConfig;
            Debug.Log("Glide Jump Selected");
        }


        if (Input.GetKeyDown("`"))
        {
            playerAnimator.enabled = !playerAnimator.enabled;
        }
        #endregion

        #region Passing to Animator

        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
        playerAnimator.SetBool("isGrounded", isGrounded);

        #endregion
    }

    /*
    private void GroundedJump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, GroundedJumpSpeed);
        soundPlayer.PlaySoundWithVariance(playerAudioSource, GroundedJumpAudioClip);
    }
    */

    IEnumerator ResetGravityToDefault(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        playerRB.gravityScale = defaultPlayerGravityScale;
    }

    private void FlipSprite(bool isFlipped)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    public void ChangeMoveState(IMoveState newState)
    {
        if (currentMoveState != null)
        {
            currentMoveState.Exit(this);
        }

        currentMoveState = newState;
        currentMoveState.Enter(this);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Main_platform"))
        {
            /*
            isGrounded = true;
            AirJumpCount = 0;
            Debug.Log("Enter Platform");
            */
            ChangeMoveState(new GroundedState());
            AirJumpCount = 0;

            //Bryan Castaneda - Only increase if it is a different platform
            if (collision.gameObject != lastPlatform)
            {
                ScoreManager.instance.IncreaseScore(); // Bryan Castaneda - Increase score of platforms jumped
            }

            lastPlatform = collision.gameObject; // Update last landed platform
            }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Main_platform"))
        {
            //isGrounded = false;

            ChangeMoveState(new AirState());

        }
    }
}
