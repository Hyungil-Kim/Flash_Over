using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPoint;


    private void Start()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPoint = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPoint = false;
    }
}
