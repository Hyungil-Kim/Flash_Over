using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChaButton : MonoBehaviour
{
    public TextMeshProUGUI chaName;
    public Image icon;
    public Image selected;
    public Button button;
    //private int chaIndex;
    public void Init(CharacterData cd,int sortindex)//, int index)
    {
        var orderType = (CharacterOrder)sortindex;
        switch (orderType)
        {
            case CharacterOrder.Default:
                chaName.text = cd.characterName;
                break;
            case CharacterOrder.Str:
                chaName.text = cd.totalStats.str.stat.ToString();
                break;
            case CharacterOrder.Hp:
                chaName.text = cd.totalStats.hp.stat.ToString();
                break;
            case CharacterOrder.Lung:
                chaName.text = cd.totalStats.lung.stat.ToString();
                break;
            case CharacterOrder.Name:
                chaName.text = cd.characterName;
                break;
            default:
                break;
        }
        if(cd.isSelected)
        {
            Selected();
        }
        else
        {
            UnSelected();
        }

        //chaIndex = index;
        //button.onClick.AddListener(() => OnChaButton());
        //icon.sprite = ***;
        //chaName = cd. ***;
    }
    public void Selected()
    {
        selected.gameObject.SetActive(true);
    }
    public void UnSelected()
    {
        selected.gameObject.SetActive(false);
    }
    //public void OnChaButton()
    //{
    //    var parent = GetComponentInParent<CharacterInfo>();
    //    //parent.currentCharacterIndex = chaIndex;
    //    parent.OnChaIcon();
    //}
}
