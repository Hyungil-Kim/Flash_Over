using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FacilityManagement : MonoBehaviour
{
    public List<GameObject> uiArray = new List<GameObject>();
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    public GameObject panel;

    public Text[] trainingDescTexts;
    public Text[] restDescTexts;
    public Text[] hireDescTexts;
    public Text[] shopDescTexts;
    public Text[] officeDescTexts;

    public Text[] shopUpgradeTexts;
    public Text[] hireUpgradeTexts;
    public Text[] triainingUpgradeTexts;
    public Text[] restUpgradeTexts;
    public Text[] officeUpgradeTexts;

    private void Start()
    {
        Init();
    }
  
    public void Init()
    {
        for (int i = 0; i < shopUpgradeTexts.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost != -1)
                    {
                        shopUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost}";
                        shopDescTexts[i].text = $"상점에 등장하는 아이템의 갯수를 늘립니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Count} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem+1).IS1Count}";
                    }
                    else
                    {
                        shopUpgradeTexts[i].text = "업그레이드 완료";
                        shopDescTexts[i].text = "상점에 등장하는 아이템의 갯수를 늘립니다.";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost != -1)
                    {
                        shopUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost}";
                        shopDescTexts[i].text = $"아이템의 가격이 줄어듭니다.\n할인율 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Sale} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale + 1).IS2Sale}";
                    }
                    else
                    {
                        shopUpgradeTexts[i].text = "업그레이드 완료";
                        shopDescTexts[i].text = "아이템의 가격이 줄어듭니다.";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost != -1)
                    {
                        shopUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost}";
                        shopDescTexts[i].text = $"상점에서 나오는 아이템의 내구도를 증가시킵니다.\n추가내구도 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Durability} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability + 1).IS3Durability}";
                    }
                    else
                    {
                        shopUpgradeTexts[i].text = "업그레이드 완료";
                        shopDescTexts[i].text = "상점에서 나오는 아이템의 내구도를 증가시킵니다.";
                    }
                    break;
                default:
                    break;
            }
            
        }
        for (int i = 0; i < triainingUpgradeTexts.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost != -1)
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost}";
                        trainingDescTexts[i].text = $"힘 훈련의 효율을 증가 시킵니다.\n획득경험치량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Str} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str+1).TS1Str}"
                            + $"훈련 비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Price} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str+1).TS1Price}";
                    }
                    else
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드완료";
                        trainingDescTexts[i].text = $"힘 훈련의 효율을 증가시킵니다.";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost != -1)
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost}";
                        trainingDescTexts[i].text = $"체력 훈련의 효율을 증가 시킵니다.\n획득경험치량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Hp} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp + 1).TS2Hp}"
                           + $"훈련 비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Price} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp + 1).TS2Price}";
                    }
                    else
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드완료";
                        trainingDescTexts[i].text = $"체력 훈련의 효율을 증가시킵니다.";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost != -1)
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost}";
                        trainingDescTexts[i].text = $"폐활량 훈련의 효율을 증가 시킵니다.\n획득경험치량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Lung} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung + 1).TS3Lung}"
                            + $"훈련 비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Price} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung + 1).TS3Price}";
                    }
                    else
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드완료";
                        trainingDescTexts[i].text = $"폐활량 훈련의 효율을 증가시킵니다.";
                    }
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < restUpgradeTexts.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost != -1)
                    {
                        restUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost}";
                        restDescTexts[i].text = $"피로회복량을 증가시킵니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Tired} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired + 1).RS1Tired}";
                    }
                    else
                    {
                        restUpgradeTexts[i].text = $"업그레이드완료";
                        restDescTexts[i].text = $"피로회복량을 증가시킵니다.";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost != -1)
                    {
                        restUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost}";
                        restDescTexts[i].text = $"심리상담의 비용을 감소시킵니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Psychological} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological + 1).RS2Psychological}";
                    }
                    else
                    {
                        restUpgradeTexts[i].text = $"업그레이드완료";
                        restDescTexts[i].text = $"심리상담의 비용을 감소시킵니다.";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost != -1)
                    {
                        restUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost}";
                        restDescTexts[i].text = $"병원진료의 비용을 감소시킵니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Physical} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical + 1).RS3Physical}";
                    }
                    else
                    {
                        restUpgradeTexts[i].text = $"업그레이드완료";
                        restDescTexts[i].text = $"병원진료의 비용을 감소시킵니다.";
                    }
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < hireUpgradeTexts.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost != -1)
                    {
                        hireUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost}";
                        hireDescTexts[i].text = $"대원선발 인원이 늘어납니다..\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Count} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade + 1).CS1Count}";
                    }
                    else
                    {
                        hireUpgradeTexts[i].text = $"업그레이드완료";
                        hireDescTexts[i].text = $"대원선발 인원이 늘어납니다.";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost != -1)
                    {
                        hireUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost}";
                        hireDescTexts[i].text = $"높은 등급의 캐릭터가 나올 수 있습니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Grade} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade + 1).CS2Grade}";
                    }
                    else
                    {
                        hireUpgradeTexts[i].text = $"업그레이드완료";
                        hireDescTexts[i].text = $"높은 등급의 캐릭터가 나올 수 있습니다.";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost != -1)
                    {
                        hireUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost}";
                        hireDescTexts[i].text = $"특성이 나올 확률이 증가합니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Characteristic} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade + 1).CS3Characteristic}";
                    }
                    else
                    {
                        hireUpgradeTexts[i].text = $"업그레이드완료";
                        hireDescTexts[i].text = $"특성이 나올 확률이 증가합니다.";
                    }
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < officeUpgradeTexts.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost != -1)
                    {
                        officeUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost}";
                        officeDescTexts[i].text = $"수용 가능 인원이 증가합니다.\n변화량 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Count} -> {MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter + 1).MC1Count}";
                    }
                    else
                    {
                        officeUpgradeTexts[i].text = $"업그레이드완료";
                        officeDescTexts[i].text = $"수용 가능 인원이 증가합니다.";
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void ShopUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost;
                    GameData.userData.itemShopData.maxItem = Mathf.Clamp(GameData.userData.itemShopData.maxItem + 1, 0,MyDataTableMgr.menuTable.tables.Count-1);
                    
                    shopUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost}";
                    if(MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost == -1)
                    {
                        shopUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.maxItem).IS1Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 1:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost;
                    GameData.userData.itemShopData.sale = Mathf.Clamp(GameData.userData.itemShopData.sale + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    shopUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost == -1)
                    {
                        shopUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 2:
                
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost;
                    GameData.userData.itemShopData.durability = Mathf.Clamp(GameData.userData.itemShopData.durability + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    shopUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost == -1)
                    {
                        shopUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            default:
                break;
        }
    }
    public void HireUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost;
                    GameData.userData.chaShopData.countUpgrade = Mathf.Clamp(GameData.userData.chaShopData.countUpgrade + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    hireUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost == -1)
                    {
                        hireUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 1:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost;
                    GameData.userData.chaShopData.gradeUpgrade = Mathf.Clamp(GameData.userData.chaShopData.gradeUpgrade + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    hireUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost == -1)
                    {
                        hireUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 2:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost;
                    GameData.userData.chaShopData.characteristicUpgrade = Mathf.Clamp(GameData.userData.chaShopData.characteristicUpgrade + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    hireUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost == -1)
                    {
                        hireUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;

            default:
                break;
        }
    }
    public void DaewonUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost 
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost;
                    GameData.userData.maxCharacter = Mathf.Clamp(GameData.userData.maxCharacter + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    officeUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost == -1)
                    {
                        officeUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            default:
                break;
        }
    }
    public void RestUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost;
                    GameData.userData.restShopData.tired = Mathf.Clamp(GameData.userData.restShopData.tired + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    restUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost == -1)
                    {
                        restUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 1:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost;
                    GameData.userData.restShopData.psychological = Mathf.Clamp(GameData.userData.restShopData.psychological + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    restUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost == -1)
                    {
                        restUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 2:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost;
                    GameData.userData.restShopData.physical = Mathf.Clamp(GameData.userData.restShopData.physical + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    restUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost == -1)
                    {
                        restUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            default:
                break;
        }
    }
    public void TrainingUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost;
                    GameData.userData.traingShopData.str = Mathf.Clamp(GameData.userData.traingShopData.str + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    triainingUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost == -1)
                    {
                        triainingUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if(GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 1:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost
                     && MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost;
                    GameData.userData.traingShopData.hp = Mathf.Clamp(GameData.userData.traingShopData.hp + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    triainingUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost == -1)
                    {
                        triainingUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            case 2:
                if (GameData.userData.gold >= MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost
                    && MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost != -1)
                {
                    GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost;
                    GameData.userData.traingShopData.lung = Mathf.Clamp(GameData.userData.traingShopData.lung + 1, 0, MyDataTableMgr.menuTable.tables.Count - 1);
                    triainingUpgradeTexts[index].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost}";

                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost == -1)
                    {
                        triainingUpgradeTexts[index].text = "업그레이드 완료";
                    }
                }
                else if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                break;
            default:
                break;
        }
        Init();
    }
    public void Open(string uiName)
    {
        panel.SetActive(false);
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
            if(ui.name == uiName)
            {
                ui.SetActive(true);
            }
        }
    }
    public void Close()
    {
        panel.SetActive(true);
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
            
        }
    }
}
