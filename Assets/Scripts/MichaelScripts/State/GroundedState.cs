//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : IMoveState
{
    private int stateType = 1;

    public void Enter(PlayerMoveDeco aPlayer)
    {
        Debug.Log("Entering Grounded State");

    }
    public int StateType()
    {
        return stateType;
    }
    public void Exit(PlayerMoveDeco aPlayer)
    {
        Debug.Log("Leaving Grounded State");
    }
}
