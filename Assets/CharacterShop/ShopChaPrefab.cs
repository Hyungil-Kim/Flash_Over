using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopChaPrefab : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI move;
    public TextMeshProUGUI str;
    public TextMeshProUGUI def;
    public TextMeshProUGUI personality;

    public void SetValue(CharacterData data)
    {
        hp.text = $"Hp : {data.totalStats.hp}";
        move.text = $"Move : {data.totalStats.move}";
        str.text = $"Str : {data.totalStats.str}";
        def.text = $"Def : {data.totalStats.def}";
        personality.text = "";
        foreach (var type in data.personality.CheakAllPersonality())
        {
            personality.text = personality.text.Insert(personality.text.Length, $"\n{type}");
        }
        personality.text = personality.text.Remove(0, 1);
    }
}
