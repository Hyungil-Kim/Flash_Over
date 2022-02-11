using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanel : MonoBehaviour
{
    public bool isPoint;
    private void Update()
    {
        isPoint = EventSystem.current.IsPointerOverGameObject();
    }
}
