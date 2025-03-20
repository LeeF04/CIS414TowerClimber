//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelRise : MonoBehaviour
{
    private GameObject playerObject;
    private Transform playerPosition;
    private Transform waterLevel;

    private float waterLevelMaximum;

    private TowerManager towerManager = new TowerManager();

    private void Start()
    {
        waterLevel = GetComponent<Transform>();
        playerObject = GameObject.Find("CatPlayer");
        playerPosition = playerObject.transform;
        waterLevelMaximum = (towerManager.towerHeight*5)-15;
    }

    void FixedUpdate()
    {
        
        float playerWaterDistance = Vector3.Distance(playerPosition.position, waterLevel.position);
        
        if (waterLevel.position.y < waterLevelMaximum) 
        {
            if (playerWaterDistance >= 10)
            {
                waterLevel.position = new Vector3(10, (waterLevel.position.y) + ((playerPosition.position.y - waterLevel.position.y) / 150), -0.25f);
            }
            else
            {
                waterLevel.position = new Vector3(10, (waterLevel.position.y) + 0.02f, -0.25f);
            }
        }
        else
        {
            Debug.Log("Water Level Maximum Reached, Y: " + waterLevelMaximum);
        }

    }
}
