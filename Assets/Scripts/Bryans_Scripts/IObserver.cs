using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void OnNotify(string eventType, object value = null);
}
