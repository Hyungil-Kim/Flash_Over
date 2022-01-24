using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireManItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI stat;
    public void Init(ItemDataBase itemData, ItemType type)
    {
        if(itemData == null)
        {
            icon.sprite = null;
            stat.text = "";
            return;
        }
        icon.sprite = itemData.dataTable.iconSprite;
        switch (type)
        {
            case ItemType.Hose:
                var hose = itemData as HoseData;
                stat.text = $"{hose.hoseData.itemName}";
                break;
            case ItemType.BunkerGear:
                var bunkerGear = itemData as BunkerGearData;
                stat.text = $"{bunkerGear.bunkerGearData.itemName}";
                break;
            case ItemType.OxygenTank:
                var oxygenTank = itemData as OxygenTankData;
                stat.text = $"{oxygenTank.oxygenTankData.itemName}";
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                var consum = itemData as ConsumableItemData;
                stat.text = $"{consum.itemData.itemName}";
                break;
            default:
                break;
        }
    }
    public void OnClick(int index)
    {
        var firetruck = GetComponentInParent<FireTruck>();
        if (firetruck.curcharacter != null)
        {
            switch (index)
            {
                case 3:
                    firetruck.OnShop();
                    break;
                case 4:
                    firetruck.OnShop();
                    break;
                default:
                    var type = (ItemType)index;
                    firetruck.OnInventory(type);
                    break;
            }
        }
    }
}
