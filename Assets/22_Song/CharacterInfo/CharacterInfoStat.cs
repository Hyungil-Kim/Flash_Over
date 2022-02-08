using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class CharacterInfoStat : MonoBehaviour
{
    public Image icon;
    public ItemButtonPrefab hose;
    public ItemButtonPrefab bunkerGear;
    public ItemButtonPrefab oxygenTank;
    public TextMeshProUGUI info;
    public TMP_InputField chaname;
    public TextMeshProUGUI hpstat;
    public TextMeshProUGUI lungstat;
    public TextMeshProUGUI strstat;
    public Slider weightslider;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI hosestat;
    public TextMeshProUGUI bunkergearstat;
    public TextMeshProUGUI oxygenstat;

    public TextMeshProUGUI personality;
    //public TextMeshProUGUI move;
    //public TextMeshProUGUI str;
    //public TextMeshProUGUI def;
    public GameObject characteristicPrefab;
    public GameObject characteristicContent;


    private void OnEnable()
    {
        Init();
    }
    private string GetGrade(ItemTableDataBase id)
    {
        var grade = "";
        switch (StringToEnum.SToE<ItemGrade>(id.grade))
        {
            case ItemGrade.Normal:
                grade = "노말";
                break;
            case ItemGrade.Rare:
                grade = "레어";
                break;
            case ItemGrade.Unique:
                grade = "유니크";
                break;
            case ItemGrade.Special:
                grade = "스페셜";
                break;
            default:
                break;
        }
        return grade;
    }
    public void Init()
    {
        var characterInfo = GetComponentInParent<CharacterInfo>();
        var curCharacter = characterInfo.curCharacter;

        if (curCharacter.hose != null)
        {
            var hoseData = curCharacter.hose.dataTable;
            hose.Init(hoseData);
            //var grade = GetGrade(hoseData);

            //hosestat.text = $"{hoseData.itemName}   {grade}\n성능 : {hoseData.dmg}\n무게 : {hoseData.weight}";
        }
        else
        {
            hose.icon.sprite = null;
            //hosestat.text = "";
        }
        if (curCharacter.bunkerGear != null)
        {
            var bunkerGearData = curCharacter.bunkerGear.dataTable;
            bunkerGear.Init(bunkerGearData);
            //var grade = GetGrade(bunkerGearData);
            //bunkergearstat.text = $"{bunkerGearData.itemName}  {grade}\n성능 : {bunkerGearData.def}\n무게 : {bunkerGearData.weight}";
        }
        else
        {
            bunkerGear.icon.sprite = null;
            //bunkergearstat.text = "";
        }
        if (curCharacter.oxygenTank != null)
        {
            var oxygenTankData = curCharacter.oxygenTank.dataTable;
            oxygenTank.Init(oxygenTankData);
            //oxygenTank.sprite = oxygenTankData.iconSprite;
            //var grade = GetGrade(oxygenTankData);
            //oxygenstat.text = $"{oxygenTankData.itemName}   {grade}\n성능 : {oxygenTankData.sta}\n무게 : {oxygenTankData.weight}";
        }
        else
        {
            oxygenTank.icon.sprite = null;
            //oxygenstat.text = "";
        }

        //info.text = $"{curCharacter.characterClass}\n{curCharacter.characterGrade}  {curCharacter.characterName}";
        chaname.text = $"{curCharacter.characterName}";
        //var statSB = new StringBuilder();
        //statSB.Append(string.Format($"Hp : {curCharacter.totalStats.hp.stat}\n"));
        //statSB.Append(string.Format($"피로도 : {curCharacter.tiredScore}"));
        //stat.text = statSB.ToString();
        hpstat.text = $"체력 : {curCharacter.totalStats.hp.stat}";
        lungstat.text = $"폐활량 : {curCharacter.totalStats.lung.stat}";
        switch (curCharacter.tiredScore /33)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
        strstat.text = $"힘 : {curCharacter.totalStats.str.stat}";

        //personality.text = "";
        //foreach (var type in curCharacter.personality.CheakAllPersonality())
        //{
        //    personality.text = personality.text.Insert(personality.text.Length, $"\n{type}");
        //}
        //personality.text = personality.text.Remove(0, 1);

        weightslider.value = ((float)curCharacter.totalStats.str.stat - (float)curCharacter.weight) / (float)curCharacter.totalStats.str.stat;
        weight.text = $"{curCharacter.totalStats.str.stat - curCharacter.weight} / {curCharacter.totalStats.str.stat}";
        //hp.text = $"Hp : {curCharacter.totalStats.hp}";
        //move.text = $"Move : {curCharacter.totalStats.move}";
        //str.text = $"Str : {curCharacter.totalStats.str}";
        //def.text = $"Lung : {curCharacter.totalStats.lung}";

        if(curCharacter.characteristics.Count >0)
        {
            foreach (var characteristic in curCharacter.characteristics)
            {
                var newGo = Instantiate(characteristicPrefab, characteristicContent.transform);
                var prefab = newGo.GetComponent<CharacteristicPrefab>();
                prefab.Init(characteristic);

            }
        }
    }
    public void test()
    {
        var characterInfo = GetComponentInParent<CharacterInfo>();
        var curCharacter = characterInfo.curCharacter;
        curCharacter.tiredScore = 70;
        Init();
    }
    public void ChangeName(string st)
    {
        var characterInfo = GetComponentInParent<CharacterInfo>();
        var curCharacter = characterInfo.curCharacter;
        curCharacter.characterName = st;
    }
}
