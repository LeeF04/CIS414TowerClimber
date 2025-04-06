// Created by Lee Fischer
// 4/6/25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLeft : ICommand
{
    private ShadowController shadow;
    public ShadowLeft(ShadowController aShadow)
    {
        this.shadow = aShadow;
    }
    public void Execute()
    {
        shadow.Move(Vector3.left);
    }
}
