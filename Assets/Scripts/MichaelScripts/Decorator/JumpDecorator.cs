//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDecorator : IJumpDecorator
{
    private readonly JumpConfig config;

    public float JumpSpeed
    {
        get {return config.JumpSpeed;}
    }
    public int JumpCountMaximum
    {
        get { return config.JumpCountMaximum; }
    }
    public int JumpType
    {
        get { return config.JumpType; }
    }
    public float HoverDuration
    {
        get { return config.HoverDuration; }
    }
    public bool Teleport
    {
        get { return config.Teleport; }
    }
    public AudioClip JumpAudio
    {
        get { return config.JumpAudio; }
    }


    public JumpDecorator(JumpConfig aJumpConfig)
    {
        this.config = aJumpConfig;
    }

}
