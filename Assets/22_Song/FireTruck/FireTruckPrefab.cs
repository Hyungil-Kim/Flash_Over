using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireTruckPrefab : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI state;
    public Button button;

    public void Init(CharacterData cd, int sortindex)
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
        //icon.sprite = cd.
        state.text = cd.state;
    }
}
