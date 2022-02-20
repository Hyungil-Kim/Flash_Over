using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public enum InfoType
{
    Character,
    Training,
    Rest,
    Truck,
}
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
    public TextMeshProUGUI movestat;
    public TextMeshProUGUI visionstat;
    public TextMeshProUGUI tiredscore;
    public TextMeshProUGUI saveClaimantCount;
    public TextMeshProUGUI clearStageCount;
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

    private List<GameObject> characteristicObject = new List<GameObject>();

    public InfoType type;
    private void OnEnable()
    {
        if(characteristicObject.Count ==0)
        {
            for (int i = 0; i < 30; i++)
            {
                var newGo = Instantiate(characteristicPrefab, characteristicContent.transform);
                characteristicObject.Add(newGo);
            }
        }
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
        //var characterInfo = GetComponentInParent<CharacterInfo>();
        CharacterData curCharacter = null;
        int curIndex = 0;
        switch (type)
        {
            case InfoType.Character:
                curCharacter = GetComponentInParent<CharacterInfo>().curCharacter;
                curIndex = GetComponentInParent<CharacterInfo>().currentIndex;
                break;
            case InfoType.Training:
                curCharacter = GetComponentInParent<TrainingCharacter>().curCharacter;
                curIndex = GetComponentInParent<TrainingCharacter>().curIndex;
                break;
            case InfoType.Rest:
                curCharacter = GetComponentInParent<Rest>().curCd;
                curIndex = GetComponentInParent<Rest>().curCharacterIndex;
                break;
            case InfoType.Truck:
                curCharacter = GetComponentInParent<FireTruck>().curcharacter;
                curIndex = GetComponentInParent<FireTruck>().curCharacterIndex;
                break;
            default:
                break;
        }

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
        UIOnOff.instance.MainCharacterSetting(curCharacter);
        icon.sprite = curCharacter.portrait;

        //var statSB = new StringBuilder();
        //statSB.Append(string.Format($"Hp : {curCharacter.totalStats.hp.stat}\n"));
        //statSB.Append(string.Format($"피로도 : {curCharacter.tiredScore}"));
        //stat.text = statSB.ToString();
        hpstat.text = $"체력 {curCharacter.totalStats.hp.stat}";
        lungstat.text = $"폐활량 {curCharacter.totalStats.lung.stat}";
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
        strstat.text = $"힘 {curCharacter.totalStats.str.stat}";

        movestat.text = $"이동력 {curCharacter.totalStats.move}";
        visionstat.text = $"시야 {curCharacter.totalStats.vision}";
        tiredscore.text = $"피로도 {curCharacter.tiredScore}";

        saveClaimantCount.text = $"{curCharacter.saveClaimantCount}";
        clearStageCount.text = $"{curCharacter.clearStageCount}";
        //personality.text = "";
        //foreach (var type in curCharacter.personality.CheakAllPersonality())
        //{
        //    personality.text = personality.text.Insert(personality.text.Length, $"\n{type}");
        //}
        //personality.text = personality.text.Remove(0, 1);

        //무게슬라이더
        //weightslider.value = ((float)curCharacter.totalStats.str.stat - (float)curCharacter.weight) / (float)curCharacter.totalStats.str.stat;
        //weight.text = $"{curCharacter.totalStats.str.stat - curCharacter.weight} / {curCharacter.totalStats.str.stat}";


        //hp.text = $"Hp : {curCharacter.totalStats.hp}";
        //move.text = $"Move : {curCharacter.totalStats.move}";
        //str.text = $"Str : {curCharacter.totalStats.str}";
        //def.text = $"Lung : {curCharacter.totalStats.lung}";

        if (characteristicObject.Count == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                var newGo = Instantiate(characteristicPrefab, characteristicContent.transform);
                characteristicObject.Add(newGo);
            }
        }
        foreach (var item in characteristicObject)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < curCharacter.characteristics.Count; i++)
        {
            var newGo = characteristicObject[i];
            var prefab = newGo.GetComponent<CharacteristicPrefab>();
            prefab.Init(curCharacter.characteristics[i]);
            newGo.SetActive(true);
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
