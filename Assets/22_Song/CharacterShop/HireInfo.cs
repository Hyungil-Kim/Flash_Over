using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HireInfo : MonoBehaviour
{
    public RawImage icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI chaGrade;
    public TextMeshProUGUI chaClass;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI move;
    public TextMeshProUGUI def;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI personality;

    public void Init(CharacterData cd)
    {
        if (cd == null)
        {
            chaName.text = "";
            chaGrade.text = "";
            chaClass.text = "";
            hp.text = "";
            move.text = "";
            def.text = "";
            stat.text = "";

            personality.text = "";
            return;
        }
        icon.texture = Resources.Load<RenderTexture>($"{cd.iconName}");
        

        chaName.text = cd.characterName;
        chaGrade.text = cd.characterGrade;
        chaClass.text = cd.characterClass;
        hp.text = $"Hp : {cd.baseStats.hp.stat}";
        move.text = $"Move : {cd.baseStats.move}";
        def.text = $"Lung : {cd.baseStats.lung.stat}";
        stat.text = $"Str : {cd.baseStats.str.stat}";

        //personality.text = "";
        //foreach (var type in cd.personality.CheakAllPersonality())
        //{
        //    personality.text = personality.text.Insert(personality.text.Length, $"\n{type}");
        //}
        //personality.text = personality.text.Remove(0, 1);
    }
}
