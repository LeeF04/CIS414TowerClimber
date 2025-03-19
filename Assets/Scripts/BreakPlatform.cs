// Written by Lee Fischer
// 3/19/25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered Collision!");
            Invoke("SetInactive", 5.0f);
        }
    }
    void SetInactive()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Platform Inactive");
    }
}
