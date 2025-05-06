// Written by Lee Fischer
// 3/11/25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject[] towerPlatforms;
    [SerializeField] private EnemyFactory enemyFactory; // Reference to factory 

    [SerializeField] private GameObject lastPlatform;

    //Edit by Michael, used to calculate maximum water level
    public int towerHeight = 25; //In platforms

    private GameObject traveller;
    
    void Awake()
    {
        traveller = new GameObject("travel");

        int randomNumber = 0;

        for(int i = 0; i < towerHeight; i++)
        {
            randomNumber = Random.Range(0, towerPlatforms.Length);

            GameObject t = Instantiate(towerPlatforms[randomNumber], traveller.transform.position, traveller.transform.rotation);


            if (enemyFactory != null && Random.value > 0.5f) //Written by Bryan Castaneda - Spawn enemy prefabs from factory
            {
                Vector3 enemySpawnPos = t.transform.position + Vector3.up * 1.5f; 
                enemyFactory.CreateEnemy(enemySpawnPos);
            }

            traveller.transform.Translate(Vector3.up * 5);
        }

        Instantiate(lastPlatform, traveller.transform.position, traveller.transform.rotation);

    }

}
