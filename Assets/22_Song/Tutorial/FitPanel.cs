using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitPanel : MonoBehaviour
{
    private RectTransform rectTr;
    public RectTransform parentRectTr;
    public RectTransform canvasTr;
    //public RectTransform canvasTr;
    void Start()
    {
        rectTr = GetComponent<RectTransform>();
        canvasTr = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        rectTr.sizeDelta = new Vector2(Screen.width, Screen.height);
        rectTr.anchoredPosition = new Vector2(-parentRectTr.anchoredPosition.x, -parentRectTr.anchoredPosition.y);
        if(canvasTr != null)
        {
            transform.position = canvasTr.transform.position;
        }
    }

    
}
