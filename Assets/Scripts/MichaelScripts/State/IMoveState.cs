//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveState
{
    void Enter(PlayerMoveDeco aPlayer);
    int StateType();
    void Exit(PlayerMoveDeco aPlayer);

}
