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
        weight.text = $"���� { itemdata.weight}";
        durability.text = $"������ {itemdata.durability}";
        switch (itemdata.type)
        {
            case ItemType.Hose:
                var hose = (HoseTableData)itemdata;
                stat.text = $"���� {hose.dmg}\n �л� {hose.burnDmg}";
                break;
            case ItemType.BunkerGear:
                var bunkerGear = (BunkerGearTableData)itemdata;
                stat.text = $"��ȭ�� {bunkerGear.def}";
                break;
            case ItemType.OxygenTank:
                var oxygenTank = (OxygenTankTableData)itemdata;
                stat.text = $"������ {oxygenTank.sta}";
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
