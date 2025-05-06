//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpDecorator
{
    float JumpSpeed {get;}
    int JumpCountMaximum {get;}
    int JumpType {get;}
    float HoverDuration {get;}
    bool Teleport {get;}
    AudioClip JumpAudio { get;}
    string JumpName { get;}
    string JumpDescription { get;}
}
