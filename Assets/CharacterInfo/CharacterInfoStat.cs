using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class CharacterInfoStat : MonoBehaviour
{
    public Image icon;
    public Image hose;
    public Image bunkerGear;
    public Image oxygenTank;
    public TextMeshProUGUI info;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI personality;
    //public TextMeshProUGUI move;
    //public TextMeshProUGUI str;
    //public TextMeshProUGUI def;

    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        var characterInfo = GetComponentInParent<CharacterInfo>();
        var curCharacter = characterInfo.curCharacter;
        
        if (curCharacter.hose != null)
        {
            hose.sprite = curCharacter.hose.dataTable.iconSprite;
        }
        else
        {
            hose.sprite = null;
        }
        if (curCharacter.bunkerGear != null)
        {
            bunkerGear.sprite = curCharacter.bunkerGear.dataTable.iconSprite;
        }
        else
        {
            bunkerGear.sprite = null;
        }
        if (curCharacter.oxygenTank != null)
        {
            oxygenTank.sprite = curCharacter.oxygenTank.dataTable.iconSprite;
        }
        else
        {
            oxygenTank.sprite = null;
        }

        info.text = $"{curCharacter.characterClass}\n{curCharacter.characterGrade}  {curCharacter.characterName}";

        var statSB = new StringBuilder();
        statSB.Append(string.Format($"Hp : {curCharacter.totalStats.hp.stat}\n"));
        statSB.Append(string.Format($"Lung : {curCharacter.totalStats.lung.stat}\n"));
        statSB.Append(string.Format($"Str : {curCharacter.totalStats.str.stat}\n"));
        statSB.Append(string.Format($"Move : {curCharacter.totalStats.move}\n"));
        statSB.Append(string.Format($"Vision : {curCharacter.totalStats.vision}\n"));
        statSB.Append(string.Format($"Dmg : {curCharacter.totalStats.dmg}\n"));
        statSB.Append(string.Format($"Def : {curCharacter.totalStats.def}\n"));
        statSB.Append(string.Format($"Sta : {curCharacter.totalStats.sta}\n"));
        statSB.Append(string.Format($"잔여무게 : {curCharacter.weight}\n"));
        statSB.Append(string.Format($"피로도 : {curCharacter.tiredScore}"));

        stat.text = statSB.ToString();

        personality.text = "";
        foreach (var type in curCharacter.personality.CheakAllPersonality())
        {
            personality.text = personality.text.Insert(personality.text.Length, $"\n{type}");
        }
        personality.text = personality.text.Remove(0, 1);

        //hp.text = $"Hp : {curCharacter.totalStats.hp}";
        //move.text = $"Move : {curCharacter.totalStats.move}";
        //str.text = $"Str : {curCharacter.totalStats.str}";
        //def.text = $"Lung : {curCharacter.totalStats.lung}";
    }
    public void test()
    {
        var characterInfo = GetComponentInParent<CharacterInfo>();
        var curCharacter = characterInfo.curCharacter;
        curCharacter.tiredScore = 70;
        Init();
    }
}
