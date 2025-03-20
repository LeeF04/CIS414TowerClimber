// Written by Lee Fischer
// 3/11/25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject[] towerPlatforms;

    //Edit by Michael, used to calculate maximum water level
    public int towerHeight = 50; //In platforms

    private GameObject traveller;
    
    void Awake()
    {
        traveller = new GameObject("travel");

        int randomNumber = 0;

        for(int i = 0; i < towerHeight; i++)
        {
            randomNumber = Random.Range(0, towerPlatforms.Length);

            GameObject t = Instantiate(towerPlatforms[randomNumber], traveller.transform.position, traveller.transform.rotation);
            traveller.transform.Translate(Vector3.up * 5);
        }
    }

}
