//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJumpConfig", menuName = "Jump/Config", order = 1)]
public class JumpConfig : ScriptableObject, IJumpDecorator
{

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private int jumpCountMaximum;

    [SerializeField]
    private int jumpType;

    [SerializeField]
    private float hoverDuration;

    [SerializeField]
    private bool teleport;

    [SerializeField]
    private AudioClip jumpAudio;

    [SerializeField] private string jumpName = "";
    [SerializeField] private string jumpDescription = "";

    public float JumpSpeed
    {
        get { return jumpSpeed; }
    }

    public int JumpCountMaximum
    {
        get { return jumpCountMaximum; }
    }

    public int JumpType
    {
        get { return jumpType; }
    }

    public float HoverDuration
    {
        get { return hoverDuration; }
    }
    public bool Teleport
    {
        get { return teleport; }
    }

    public AudioClip JumpAudio
    {
        get { return jumpAudio; }
    }

    public string JumpName
    {
        get { return jumpName; }
    }

    public string JumpDescription
    {
        get { return jumpDescription; }
    }

}
