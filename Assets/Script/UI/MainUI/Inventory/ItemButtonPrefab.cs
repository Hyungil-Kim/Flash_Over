using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButtonPrefab : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI durability;
    public TextMeshProUGUI stat;
    public void Init(ItemTableDataBase itemdata)
    {
        icon.sprite = itemdata.iconSprite;
        itemName.text = itemdata.itemName;
        weight.text = $"무게 { itemdata.weight}";
        durability.text = $"내구도 {itemdata.durability}";
        switch (itemdata.type)
        {
            case ItemType.Hose:
                var hose = (HoseTableData)itemdata;
                stat.text = $"직사 {hose.dmg}\n 분사 {hose.burnDmg}";
                break;
            case ItemType.BunkerGear:
                var bunkerGear = (BunkerGearTableData)itemdata;
                stat.text = $"방화력 {bunkerGear.def}";
                break;
            case ItemType.OxygenTank:
                var oxygenTank = (OxygenTankTableData)itemdata;
                stat.text = $"충전량 {oxygenTank.sta}";
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                break;
            default:
                break;
        }
    }
}
