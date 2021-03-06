﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(bool hit);

}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void RegisterObserver (Observer observer)
    {
        _observers.Add(observer);
    }

    public void Notify(bool hit)
    {
        foreach (var observer in _observers)
            observer.OnNotify(hit);
    }
}
