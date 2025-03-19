//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpingStrategy
{
    void SecondJump(Rigidbody2D playerRB);
    int SecondJumpMaximum();
}
