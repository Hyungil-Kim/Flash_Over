using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireTruckItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    private ItemDataBase itemData;
    private ItemType itemType;
    public void Init(ItemDataBase item,ItemType type)
    {
        itemData = item;
        itemType = type;
        icon.sprite = item.dataTable.iconSprite;
        itemName.text = item.dataTable.itemName;
    }
    public void OnClick()
    {
        var firemanInfo = GetComponentInParent<FireManInfo>();
        switch (itemType)
        {
            case ItemType.Consumable:
                var consum1 = firemanInfo.CurCharacter.consum1;
                var consum2 = firemanInfo.CurCharacter.consum2;
                if (itemType == ItemType.Consumable)
                {
                    if (consum1 != null || consum2 != null)
                    {
                        if (consum1.dataTable.itemName == itemData.dataTable.itemName)
                        {
                            consum1.count++;
                        }
                        else if (consum2.dataTable.itemName == itemData.dataTable.itemName)
                        {
                            consum2.count++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (consum1 == null)
                    {
                        consum1 = itemData as ConsumableItemData;
                        consum1.count = 1;
                    }
                    else if (consum2 == null)
                    {
                        consum2 = itemData as ConsumableItemData;
                        consum2.count = 1;
                    }
                }
                break;
            default:
                firemanInfo.CurCharacter.EquipItem(itemData, itemType);
                firemanInfo.CharacterInit();
                break;
        }
    }
}
