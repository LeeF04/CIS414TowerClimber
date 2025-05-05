using System.Collections.Generic;
using UnityEngine;

public class GameEventSubject : MonoBehaviour, ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    public void NotifyObservers(string eventType, object value = null)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(eventType, value);
        }
    }
}
