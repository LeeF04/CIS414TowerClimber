// Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{

    // Moving
    [SerializeField] private float moveSpeed = 5.0f;
    private float direction = 0.0f;

    // Player X Flipping
    private bool isFlipped = false;

    // Rigidbody2D
    private Rigidbody2D playerRB;

    // Gravity Scale
    [SerializeField] private float defaultPlayerGravityScale = 1.5f;

    // Jumping
    private IJumpingStrategy jumpingStrategy;
    private bool isGrounded = true;
    private int AirJumpType = 1;
    private int AirJumpCount = 0;

    // Grounded Jump
    [SerializeField] private AudioClip GroundedJumpAudioClip;
    [SerializeField] private float GroundedJumpSpeed = 10.0f;

    // Double Jump
    [SerializeField] private AudioClip DoubleJumpAudioClip;

    // Teleport Jump
    [SerializeField] private AudioClip TeleportJumpAudioClip;

    // Triple Jump
    [SerializeField] private AudioClip TripleJumpAudioClip;

    // Hover Jump
    [SerializeField] private AudioClip HoverJumpAudioClip;
    [SerializeField] private float hoverDuration = 0.5f;

    // Sound
    [SerializeField] AudioSource playerAudioSource;
    SoundPlayer soundPlayer = new SoundPlayer();

    //Animation
    private Animator playerAnimator;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        jumpingStrategy = new Default2Jump();
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
            if (isGrounded)
            {
                GroundedJump();
                Debug.Log("Grounded Jump");
            }
            else
            {
                if (AirJumpType != 0)
                { 
                    if (AirJumpCount < jumpingStrategy.SecondJumpMaximum())
                    {
                        jumpingStrategy.SecondJump(playerRB);
                        Debug.Log("Double Jump");
                        AirJumpCount++;

                        switch (AirJumpType)
                        {
                            case 1:
                                soundPlayer.PlaySoundWithVariance(playerAudioSource, DoubleJumpAudioClip);
                                break;

                            case 2:
                                soundPlayer.PlaySoundWithVariance(playerAudioSource, TeleportJumpAudioClip);
                                break;

                            case 3:
                                soundPlayer.PlaySoundWithVariance(playerAudioSource, TripleJumpAudioClip);
                                break;

                            case 4:
                                soundPlayer.PlaySoundWithVariance(playerAudioSource, HoverJumpAudioClip);
                                StartCoroutine(ResetGravityToDefault());
                                break;

                            default:
                                soundPlayer.PlaySoundWithVariance(playerAudioSource, DoubleJumpAudioClip);
                                Debug.Log("Used Default AirJumpType in switch case statement");
                                break;
                        }
                    }
                }
                else if (AirJumpType == 0)
                {
                    Debug.Log("No AirJumpType Selected");
                }
                else 
                {
                    Debug.Log("Invaild AirJumpType selected");
                }
                
            }
        }
        #endregion

        #region GetKetDown
        if (Input.GetKeyDown("0"))
        {
            jumpingStrategy = new Default2Jump();
            AirJumpType = 0;
        }

        if (Input.GetKeyDown("1"))
        {
            jumpingStrategy = new Default2Jump();
            AirJumpType = 1;
        }

        if (Input.GetKeyDown("2"))
        {
            jumpingStrategy = new Teleport2Jump();
            AirJumpType = 2;
        }

        if (Input.GetKeyDown("3"))
        {
            jumpingStrategy = new Triple2Jump();
            AirJumpType = 3;
        }

        if (Input.GetKeyDown("4"))
        {
            jumpingStrategy = new Hover2Jump();
            AirJumpType = 4;
        }
        #endregion

        #region Passing to Animator

        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
        playerAnimator.SetBool("isGrounded", isGrounded);

        #endregion
    }

    private void GroundedJump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, GroundedJumpSpeed);
        soundPlayer.PlaySoundWithVariance(playerAudioSource, GroundedJumpAudioClip);
    }

    IEnumerator ResetGravityToDefault()
    {
        yield return new WaitForSeconds(hoverDuration);

        playerRB.gravityScale = defaultPlayerGravityScale;
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
            AirJumpCount = 0;
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
