using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChaButton : MonoBehaviour
{
    public TextMeshProUGUI chaName;
    public Image icon;
    public Button button;
    //private int chaIndex;
    public void Init(CharacterData cd)//, int index)
    {
        chaName.text = cd.totalStats.str.ToString();
        //chaIndex = index;
        //button.onClick.AddListener(() => OnChaButton());
        //icon.sprite = ***;
        //chaName = cd. ***;
    }
    //public void OnChaButton()
    //{
    //    var parent = GetComponentInParent<CharacterInfo>();
    //    //parent.currentCharacterIndex = chaIndex;
    //    parent.OnChaIcon();
    //}
}
