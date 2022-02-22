using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Subject
{
    void AddObserver(Observer observer);
    void RemoveObserver(Observer observer);
    void Notify();
}
