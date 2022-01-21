using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoInventoryItem : MonoBehaviour
{
    public Text itemName;
    public Text itemValue;
    public Image icon;
    public ItemDataBase itemData;
    public GameObject equipImage;
    public ItemType itemType;

    public void Init(ItemDataBase data ,ItemType type)
    {
        itemData = data;
        itemType = type;
        
        icon.sprite = itemData.dataTable.iconSprite;
        itemName.text = itemData.dataTable.itemName;
        switch (itemType)
        {
            case ItemType.Hose:
                itemValue.text = $"공격력 : {itemData.dataTable.dmg}";
                break;
            case ItemType.BunkerGear:
                itemValue.text = $"방어력 : {itemData.dataTable.def}";
                break;
            case ItemType.OxygenTank:
                itemValue.text = $"스태미너 : {itemData.dataTable.sta}";
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                break;  
            default:
                break;
        }

        equipImage.SetActive(false);
        if (itemData.owner != null)
        {
            equipImage.SetActive(true);
        }

    }
    public void OnEquipButton()
    {
        var parent = GetComponentInParent<CharacterInfo>();
        var inventory = GetComponentInParent<CharacterInfoInventory>();
        var character = parent.curCharacter;
        if (itemData != null)
        {
            var equipWeight = 0;
            switch (itemType)
            {
                case ItemType.Hose:
                    if (character.hose != null)
                    {
                        equipWeight = character.hose.dataTable.weight;
                    }
                    break;
                case ItemType.BunkerGear:
                    if (character.bunkerGear != null)
                    {
                        equipWeight = character.bunkerGear.dataTable.weight;
                    }
                    break;
                case ItemType.OxygenTank:
                    if (character.oxygenTank != null)
                    {
                        equipWeight = character.oxygenTank.dataTable.weight;
                    }
                    break;
                case ItemType.Max:
                    break;
                case ItemType.Consumable:
                    break;
                default:
                    break;
            }

            if (character.weight + equipWeight < itemData.dataTable.weight)
            {
                inventory.WeightFull();
                //무게 부족
                
                return;
            }
        }
        if (itemData != null && itemData.owner != null)
        {
            inventory.AlreadyEquip(character, itemData, itemType);
        }
        else if (itemData != null)
        {
            character.EquipItem(itemData, itemType);
            parent.OnExitInventory();
        }
        else
        {
            character.DisarmItem(itemType);
            parent.OnExitInventory();
        }
    }
    //public void OnItemIcon()
    //{
    //    //좀 이상함
    //    var parent =GetComponentInParent<CharacterInfoInventory>();
    //    parent.itemInfo.Init(itemData, itemType);
    //}
}
