using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public bool isEmpty;
    public bool next;
    public TutorialPanel[] panals;
    public GameObject hidepanals;
    public void Open()
    {
        hidepanals.SetActive(false);
    }
    public void Hide()
    {
        hidepanals.SetActive(true);
    }
    public bool CheckNext()
    {
        next = false;
        if (!isEmpty)
        {
            next = UIOnOff.instance.ontouch;
        }
        else
        {
            bool inPoint = false;
            foreach (var item in panals)
            {
                if (UIOnOff.instance.ontouch && item.isPoint)
                {
                    inPoint = true;
                    break;
                }
            }
            if (!inPoint)
            {
                next = UIOnOff.instance.ontouch;
            }
        }
        return next;
    }
}
