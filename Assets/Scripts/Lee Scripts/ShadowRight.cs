// Created by Lee Fischer
// 4/6/25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRight : ICommand
{
    private ShadowController shadow;
    public ShadowRight(ShadowController aShadow)
    {
        this.shadow = aShadow;
    }
    public void Execute()
    {
        shadow.Move(Vector3.right);
    }
}
