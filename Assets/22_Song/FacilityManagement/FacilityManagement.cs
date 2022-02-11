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
                    }
                    else
                    {
                        shopUpgradeTexts[i].text = "업그레이드 완료";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost != -1)
                    {
                        shopUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.sale).IS2Cost}";
                    }
                    else
                    {
                        shopUpgradeTexts[i].text = "업그레이드 완료";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost != -1)
                    {
                        shopUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.itemShopData.durability).IS3Cost}";
                    }
                    else
                    {
                        shopUpgradeTexts[i].text = "업그레이드 완료";
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
                    }
                    else
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드완료";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost != -1)
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Cost}";
                    }
                    else
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드완료";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost != -1)
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Cost}";
                    }
                    else
                    {
                        triainingUpgradeTexts[i].text = $"업그레이드완료";
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
                    }
                    else
                    {
                        restUpgradeTexts[i].text = $"업그레이드완료";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost != -1)
                    {
                        restUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Cost}";
                    }
                    else
                    {
                        restUpgradeTexts[i].text = $"업그레이드완료";
                    }
                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost != -1)
                    {
                        restUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Cost}";
                    }
                    else
                    {
                        restUpgradeTexts[i].text = $"업그레이드완료";
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
                    }
                    else
                    {
                        hireUpgradeTexts[i].text = $"업그레이드완료";
                    }
                    break;
                case 1:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost != -1)
                    {
                        hireUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.gradeUpgrade).CS2Cost}";
                    }
                    else
                    {
                        hireUpgradeTexts[i].text = $"업그레이드완료";
                    }

                    break;
                case 2:
                    if (MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost != -1)
                    {
                        hireUpgradeTexts[i].text = $"업그레이드\n비용 : {MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.characteristicUpgrade).CS3Cost}";
                    }
                    else
                    {
                        hireUpgradeTexts[i].text = $"업그레이드완료";
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
                    }
                    else
                    {
                        officeUpgradeTexts[i].text = $"업그레이드완료";
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
