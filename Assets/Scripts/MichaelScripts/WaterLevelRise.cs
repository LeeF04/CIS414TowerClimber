//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelRise : MonoBehaviour
{

    private Transform waterLevel;

    private void Start()
    {
        waterLevel = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        waterLevel.position = new Vector3(10, (waterLevel.position.y)+0.03f, -0.25f);
    }
}
