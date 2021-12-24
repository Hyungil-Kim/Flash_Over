using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopChaPrefab : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI chaGrade;
    public TextMeshProUGUI chaClass;

    public void SetValue(CharacterData cd)
    {
        chaName.text = cd.characterName;
        chaGrade.text = cd.characterGrade;
        chaClass.text = cd.characterClass;
    }
}
