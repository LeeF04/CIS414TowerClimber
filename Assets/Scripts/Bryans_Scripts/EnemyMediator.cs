using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMediator : MonoBehaviour, IEnemyMediator
{
    private List<Enemy_Patrol> enemies = new List<Enemy_Patrol>();

    public void RegisterEnemy(Enemy_Patrol enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public Vector3 GetGroupCenter()
    {
        if (enemies.Count == 0) return Vector3.zero;

        Vector3 total = Vector3.zero;
        foreach (var e in enemies)
            total += e.transform.position;

        return total / enemies.Count;
    }
}
