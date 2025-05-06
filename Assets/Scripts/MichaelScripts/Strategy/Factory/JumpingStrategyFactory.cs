//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JumpingStrategyFactory
{
    public static IJumpingStrategy GetStrategy(KeyCode keyPressed)
    {
        IJumpingStrategy strategy = null;

        switch (keyPressed)
        {
            case KeyCode.Alpha0:
                strategy = null;
                break;
            case KeyCode.Alpha1:
                strategy = new Default2Jump();
                break;
            case KeyCode.Alpha2:
                strategy = new Teleport2Jump();
                break;
            case KeyCode.Alpha3:
                strategy = new Triple2Jump();
                break;
            case KeyCode.Alpha4:
                strategy = new Hover2Jump();
                break;
            default:
                strategy = new Default2Jump();
                break;
        }
        return strategy;
    }
}