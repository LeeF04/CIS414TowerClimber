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
    //private int AirJumpType = 1;
    private int AirJumpCount = 0;

    private GameObject lastPlatform; // Bryan Castaneda - Stores the last platform the player landed on this is for score counter
    private bool platformScored = false;

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

    // Keys
    private KeyCode KeyFromKeyPressed = 0;

    // SaveSystem - Lee F.
    private ISaveSystem saveSystem;
    private bool isLoading = false;

    //Jump Type UI
    private UI_JumpTypeManager jumpTypeManager;// = new UI_JumpTypeManager();

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        jumpingStrategy = new Default2Jump();

        // Adding in SaveSystem - Lee F.
        saveSystem = new CSVSaveAdapter();

        jumpTypeManager = FindObjectOfType<UI_JumpTypeManager>();
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
                if (jumpingStrategy.AirJumpType() != 0)
                { 
                    if (AirJumpCount < jumpingStrategy.SecondJumpMaximum())
                    {
                        jumpingStrategy.SecondJump(playerRB);
                        Debug.Log("Double Jump");
                        AirJumpCount++;

                        switch (jumpingStrategy.AirJumpType())
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
                else if (jumpingStrategy.AirJumpType() == 0)
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

        KeyCode[] keysToCheck = { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

        if (KeyPressed(keysToCheck))
        {
            Debug.Log("Specified key has been pressed.");
            IJumpingStrategy newStrategy = JumpingStrategyFactory.GetStrategy(KeyFromKeyPressed);
            ChangeMovementStrategy(newStrategy);

            if (newStrategy != null)
            {
                switch (jumpingStrategy.AirJumpType())
                {
                    case 1:
                        jumpTypeManager.UpdateMoveTypeText("Double"); 
                        break;

                    case 2:
                        jumpTypeManager.UpdateMoveTypeText("Teleport");
                        break;

                    case 3:
                        jumpTypeManager.UpdateMoveTypeText("Triple");
                        break;

                    case 4:
                        jumpTypeManager.UpdateMoveTypeText("Glide");
                        break;

                    default:
                        jumpTypeManager.UpdateMoveTypeText("Double");
                        Debug.Log("Used Default AirJumpType in switch case statement");
                        break;
                }
            }
            else
            {
                jumpTypeManager.UpdateMoveTypeText("None");
                Debug.Log("Jump Strategy is NULL");
            }
            
        }


        /*
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
        */

        if (Input.GetKeyDown("`"))
        {
            playerAnimator.enabled = !playerAnimator.enabled;
        }
        #endregion

        #region Passing to Animator

        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
        playerAnimator.SetBool("isGrounded", isGrounded);

        #endregion

        // Keys For Saving - Lee F.
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    bool KeyPressed(KeyCode[] keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log("Key Pressed = " + key);
                KeyFromKeyPressed = key;
                return true;
            }
        }
        return false;
    }

    public void ChangeMovementStrategy(IJumpingStrategy newStrategy)
    {
        this.jumpingStrategy = newStrategy;
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Platform"))
    //    {
    //        isGrounded = true;
    //        AirJumpCount = 0;
    //        Debug.Log("Enter Platform");

    //        //Bryan Castaneda - Only increase if it is a different platform
    //        if (collision.gameObject != lastPlatform || !platformScored)
    //        {
    //            ScoreManager.Instance.IncreaseScore();
    //            platformScored = true;
    //        }

    //        lastPlatform = collision.gameObject; // Update last landed platform
    //        }
            
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            platformScored = false;
            Debug.Log("Exit Platform");
        }
    }
    void SaveData()
    {
        Debug.Log("Saving...");

        PlayerData data = new PlayerData
        {
            position = transform.position
        };

        saveSystem.SavePlayerData(data);
    }
    void LoadData()
    {
        isLoading = true;
        PlayerData data = saveSystem.LoadPlayerData();
        transform.position = data.position;
        isLoading = false;
    }
}
