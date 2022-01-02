using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using System.Text;
public class FireManInfoPrefab : MonoBehaviour
{
    public Image icon;
    public FireManItem hose;
    public FireManItem bunkerGear;
    public FireManItem oxygenTank;
    public TextMeshProUGUI info;
    public TextMeshProUGUI stat;
    //public TextMeshProUGUI personality;
    public void Init(CharacterData cd)
    {
        hose.Init(cd.hose, ItemType.Hose);
        bunkerGear.Init(cd.bunkerGear, ItemType.BunkerGear);
        oxygenTank.Init(cd.oxygenTank, ItemType.OxygenTank);

        info.text = $"{cd.characterClass}\n{cd.characterGrade}  {cd.characterName}";

        var statSB = new StringBuilder();
        statSB.Append(string.Format($"Hp : {cd.totalStats.hp.stat}\n"));
        statSB.Append(string.Format($"Lung : {cd.totalStats.lung.stat}\n"));
        statSB.Append(string.Format($"Str : {cd.totalStats.str.stat}\n"));
        statSB.Append(string.Format($"Move : {cd.totalStats.move}\n"));
        statSB.Append(string.Format($"Vision : {cd.totalStats.vision}\n"));
        statSB.Append(string.Format($"Dmg : {cd.totalStats.dmg}\n"));
        statSB.Append(string.Format($"Def : {cd.totalStats.def}\n"));
        statSB.Append(string.Format($"Sta : {cd.totalStats.sta}"));
        stat.text = statSB.ToString();

    }
}
