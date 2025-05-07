using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMediator
{ 
    public void RegisterEnemy(Enemy_Patrol enemy);
    Vector3 GetGroupCenter();
}
