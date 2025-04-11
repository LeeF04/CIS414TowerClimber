//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IMoveState
{
    private int stateType = 2;

    public void Enter(PlayerMoveDeco aPlayer)
    {
        Debug.Log("Entering Air State");

    }
    public int StateType()
    {
        return stateType;
    }
    public void Exit(PlayerMoveDeco aPlayer)
    {
        Debug.Log("Leaving Air State");
    }
}
