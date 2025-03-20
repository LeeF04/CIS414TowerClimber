using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Bryan Castaneda
//Factory Pattern
public class EnemyFactory : MonoBehaviour
{
    public GameObject axeEnemyPrefab;
    public GameObject macePrefab;
    public GameObject sawPrefab;

    public GameObject CreateEnemy(Vector3 position)
    {
        int randomEnemy = Random.Range(0, 3); // 3 enemy types

        GameObject enemyToSpawn = null;

        switch (randomEnemy)
        {
            case 0:
                enemyToSpawn = Instantiate(axeEnemyPrefab, position, Quaternion.identity);
                break;
            case 1:
                enemyToSpawn = Instantiate(macePrefab, position, Quaternion.identity);
                break;
            case 2:
                enemyToSpawn = Instantiate(sawPrefab, position, Quaternion.identity);
                break;
        }

        return enemyToSpawn;
    }
}