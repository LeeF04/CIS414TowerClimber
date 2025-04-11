// Written by Lee Fischer
// 3/19/25

//Editted by Michael

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    //Michael's change
    [SerializeField] private float platformInactiveTimer = 15.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered Collision!");
            Invoke("SetInactive", platformInactiveTimer);
        }
    }
    void SetInactive()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Platform Inactive");
    }
}
