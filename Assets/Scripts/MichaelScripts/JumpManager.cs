// Michael Anglemier
//
// Unused

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    // Player
    //[SerializeField] private Rigidbody2D playerRB;
    private float defaultPlayerGravityScale = 1.5f;

    // Jumping Strategy
    private IJumpingStrategy jumpingStrategy;

    // AirJump Type
    //private int AirJumpType;

    // Sound
    [SerializeField] AudioSource playerAudioSource;
    SoundPlayer soundPlayer = new SoundPlayer();

    // Ground Jump
    [SerializeField] private AudioClip GroundJumpAudioClip;
    [SerializeField] private float GroundJumpSpeed = 10.0f;

    // Double Jump
    [SerializeField] private AudioClip DoubleJumpAudioClip;
    [SerializeField] private int DoubleJumpCount = 0;
    [SerializeField] private int DoubleJumpMaximum = 1;
    [SerializeField] private float DoubleJumpSpeed = 10.0f;

    // Teleport Jump
    [SerializeField] private AudioClip TeleportJumpAudioClip;
    [SerializeField] private int TeleportJumpCount = 0;
    [SerializeField] private int TeleportJumpMaximum = 1;
    private Transform playerPosition;

    // Triple Jump
    [SerializeField] private AudioClip TripleJumpAudioClip;
    [SerializeField] private int TripleJumpCount = 0;
    [SerializeField] private int TripleJumpMaximum = 2;
    private int TripleJumpSpeed = 8;

    // Hover Jump
    [SerializeField] private AudioClip HoverJumpAudioClip;
    [SerializeField] private int HoverJumpCount = 0;
    [SerializeField] private int HoverJumpMaximum = 1;
    [SerializeField] private float hoverDuration = 0.5f;


    public void GroundedJump(Rigidbody2D playerRB)
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, GroundJumpSpeed);
        soundPlayer.PlaySoundWithVariance(playerAudioSource, GroundJumpAudioClip);

        DoubleJumpCount = 0;
        TeleportJumpCount = 0;
        TripleJumpCount = 0;
        HoverJumpCount = 0;

        Debug.Log("JumpManager.GroundedJump");
    }

    public void AirJump(int AirJumpType, Rigidbody2D playerRB)
    {
        Debug.Log("AirJumpType: " + AirJumpType);

        if (AirJumpType != 0)
        {
            switch (AirJumpType)
            {
                case 1:

                    //Attempted to debug and find where my NullReference was

                    /*
                    if (playerRB == null)
                    {
                        Debug.Log("Case 1 playerRB == null");
                    }
                    if (playerRB != null)
                    {
                        Debug.Log("Case 1 playerRB != null");
                    }
                    */

                    DoubleJump(playerRB);
                    break;

                case 2:
                    TeleportJump(playerRB);
                    break;

                case 3:
                    TripleJump(playerRB);
                    break;

                case 4:
                    HoverJump(playerRB);
                    break;

                default:
                    DoubleJump(playerRB);
                    Debug.Log("Used Default AirJumpType in switch case statement");
                    break;
            }
        }
        else
        {
            Debug.Log("No valid AirJumpType selected");
        }
    }

    public void DoubleJump(Rigidbody2D playerRB)
    {

        //Attempted to debug and find where my NullReference was

        /*
        if (playerRB == null)
        {
            Debug.Log("Double playerRB == null");
        }
        if (playerRB != null)
        {
            Debug.Log("Double playerRB != null");
            Debug.Log(playerRB);
        }
        */

        if (DoubleJumpCount < DoubleJumpMaximum)
        {
            //jumpingStrategy.SecondJump(playerRB);

            /*
             * As far as I can tell it is the above line that fails in conjunction with my Strategy pattern
             * I am unsure why but it insists that "playerRB" is null here despite me using Debug sttements to print it
             * out and see that it isn't set to null its set to "CatPlayer" as it should be
             */

            //Below is the code that should execute but I couldn't get the above line to work

            playerRB.velocity = new Vector2(playerRB.velocity.x, DoubleJumpSpeed);

            //

            soundPlayer.PlaySoundWithVariance(playerAudioSource, DoubleJumpAudioClip);
            DoubleJumpCount++;

            Debug.Log("JumpManager.DoubleJump " + DoubleJumpCount + " " + DoubleJumpMaximum);
        }
        else
        {
            Debug.Log("JumpManager.DoubleJump failed to jump");
        }

    }

    public void TeleportJump(Rigidbody2D playerRB)
    {
        if (TeleportJumpCount < TeleportJumpMaximum)
        {
            //jumpingStrategy.SecondJump(playerRB);

            //Below is the code that should execute but I couldn't get the above line to work

            playerPosition = playerRB.GetComponent<Transform>();
            playerPosition.transform.position = new Vector3(-playerPosition.transform.position.x, playerPosition.transform.position.y + 5, playerPosition.transform.position.z);

            //

            soundPlayer.PlaySoundWithVariance(playerAudioSource, TeleportJumpAudioClip);
            TeleportJumpCount++;
        
            Debug.Log("JumpManager.TeleportJump " + TeleportJumpCount + " " + TeleportJumpMaximum);
        }
        else
        {
            Debug.Log("JumpManager.TeleportJump failed to jump");
        }
    }

    public void TripleJump(Rigidbody2D playerRB)
    {
        if (TripleJumpCount < TripleJumpMaximum)
        {
            //jumpingStrategy.SecondJump(playerRB);

            //Below is the code that should execute but I couldn't get the above line to work

            playerRB.velocity = new Vector2(playerRB.velocity.x, TripleJumpSpeed);

            //

            soundPlayer.PlaySoundWithVariance(playerAudioSource, TripleJumpAudioClip);
            TripleJumpCount++;

            Debug.Log("JumpManager.TripleJump " + TripleJumpCount + " " + TripleJumpMaximum);
        }
        else
        {
            Debug.Log("JumpManager.TripleJump failed to jump");
        }

    }

    public void HoverJump(Rigidbody2D playerRB)
    {
        if (HoverJumpCount < HoverJumpMaximum)
        {
            //jumpingStrategy.SecondJump(playerRB);

            //Below is the code that should execute but I couldn't get the above line to work

            playerRB.gravityScale = 0;

            //

            soundPlayer.PlaySoundWithVariance(playerAudioSource, HoverJumpAudioClip);
            HoverJumpCount++;

            resetGravity(playerRB);

            Debug.Log("JumpManager.HoverJump " + HoverJumpCount + " " + HoverJumpMaximum);
        }
        else
        {
            Debug.Log("JumpManager.HoverJump failed to jump");
        }



    }

    
    IEnumerator resetGravity(Rigidbody2D playerRB)
    {
        yield return new WaitForSeconds(hoverDuration);

        playerRB.gravityScale = defaultPlayerGravityScale;
    }
    


}
