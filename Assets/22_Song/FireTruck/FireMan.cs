using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FireMan : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI fireManName;
    public CharacterData cd;
    FireTruck fireTruck;
    

    private void OnEnable()
    {
        fireTruck = GetComponentInParent<FireTruck>();
        cd = null;
        Init();
    }
    public void Init()
    {
        if (cd == null)
        {
            icon.sprite = null;
            fireManName.text = "";
            return;
        }
        else
        {
            fireManName.text = cd.characterName;
        }
    }

}
