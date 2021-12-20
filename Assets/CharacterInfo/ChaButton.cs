using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChaButton : MonoBehaviour
{
    public TextMeshProUGUI chaName;
    Image icon;
    
    public void Init(CharacterData data)
    {
        icon = GetComponent<Image>();
        //icon.sprite = ***;
        //chaName = data. ***;
    }
    public void OnChaButton(int index)
    {
        var parent = GetComponentInParent<CharacterInfo>();
        parent.currentCharacterIndex = index;
        parent.OnChaIcon();
    }
}
