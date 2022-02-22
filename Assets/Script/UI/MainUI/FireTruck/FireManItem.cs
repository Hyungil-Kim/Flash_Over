using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireManItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public Sprite sprite;
    public void Init(ItemDataBase itemData, ItemType type)
    {
        if(itemData == null)
        {
            icon.sprite = sprite;
            itemName.text = "";
            itemDesc.text = "";
            return;
        }
        icon.sprite = itemData.dataTable.iconSprite;
        switch (type)
        {
            case ItemType.Hose:
                var hose = itemData as HoseData;
                itemName.text = $"{hose.hoseData.itemName}";
                itemDesc.text = $"내구도 {hose.hoseData.durability}";
                break;
            case ItemType.BunkerGear:
                var bunkerGear = itemData as BunkerGearData;
                itemName.text = $"{bunkerGear.bunkerGearData.itemName}";
                itemDesc.text = $"내구도 {bunkerGear.bunkerGearData.durability}";
                break;
            case ItemType.OxygenTank:
                var oxygenTank = itemData as OxygenTankData;
                itemName.text = $"{oxygenTank.oxygenTankData.itemName}";
                itemDesc.text = $"내구도 {oxygenTank.oxygenTankData.durability}";
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                var consum = itemData as ConsumableItemData;
                itemName.text = $"{consum.itemData.itemName}";
                itemDesc.text = $"수량 {consum.count}";
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
