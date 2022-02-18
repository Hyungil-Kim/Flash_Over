using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public bool isEmpty;
    public bool needHide;

    public GameObject needHideObj;


    public bool next;
    public TutorialPanel touchpanal;
    public GameObject hidepanals;

    private void OnEnable()
    {
        if(needHide)
        {
            needHideObj.SetActive(true);
        }
    }
    public void HideObj()
    {
        if (needHide)
        {
            needHideObj.SetActive(false);
        }
    }
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
            if(UIOnOff.instance.ontouch && touchpanal.isPoint)
            {
                inPoint = true;
            }
            //foreach (var item in panals)
            //{
            //    if (UIOnOff.instance.ontouch && item.isPoint)
            //    {
            //        inPoint = true;
            //        break;
            //    }
            //}
            if (inPoint)
            {
                next = UIOnOff.instance.ontouch;
            }
        }
        return next;
    }
    public void GoNext()
    {
        next = true;
    }
}
