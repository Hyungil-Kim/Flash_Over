using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    public Image icon;
    void Update()
    {
        if (GameManager.instance.changePlayer != null)
            Init();
    }
    public void Init()
    {
        var pos = GameManager.instance.mousePos;
        
        GetComponent<RectTransform>().position = pos;
    }
}
