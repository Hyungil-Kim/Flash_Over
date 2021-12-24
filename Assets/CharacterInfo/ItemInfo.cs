using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemInfo : MonoBehaviour
{
    public AlreadyEquip alreadyEquip;
    public Image icon;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI description;
    private ItemDataBase itemData;
    private ItemType itemType;

    public void Init(ItemDataBase data, ItemType type)
    {
        itemData = data;
        itemType = type;
        icon.color = Color.white;
        switch (type)
        {
            case ItemType.Hose:
                var hose = data as HoseData;
                icon.sprite = data.dataTable.iconSprite;
                stat.text = "";
                foreach (var enumValue in System.Enum.GetValues(typeof(CharacterStatType)))
                {
                    var statType = (CharacterStatType)enumValue;
                    if (itemData.GetStat(statType) != 0)
                    {
                        stat.text = stat.text.Insert(stat.text.Length, $"\n{statType.ToString()} : {itemData.GetStat(statType).ToString()}");
                    }
                }
                break;
            case ItemType.BunkerGear:
                var bunkerGear = data as BunkerGearData;
                icon.sprite = data.dataTable.iconSprite;
                stat.text = "";
                foreach (var enumValue in System.Enum.GetValues(typeof(CharacterStatType)))
                {
                    var statType = (CharacterStatType)enumValue;
                    if (itemData.GetStat(statType) != 0)
                    {
                        stat.text = stat.text.Insert(stat.text.Length, $"\n{statType.ToString()} : {itemData.GetStat(statType).ToString()}");
                    }
                }
                break;
            case ItemType.OxygenTank:
                var oxygenTank = data as OxygenTankData;
                icon.sprite = data.dataTable.iconSprite;
                stat.text = "";
                foreach (var enumValue in System.Enum.GetValues(typeof(CharacterStatType)))
                {
                    var statType = (CharacterStatType)enumValue;
                    if (itemData.GetStat(statType) != 0)
                    {
                        stat.text = stat.text.Insert(stat.text.Length, $"\n{statType.ToString()} : {itemData.GetStat(statType).ToString()}");
                    }
                }
                break;
            default:
                break;
        }
        stat.text.Remove(0, 1);
        description.text = "";
    }
    public void None(ItemType type)
    {
        itemData = null;
        itemType = type;
        icon.sprite = null;
        icon.color = Color.clear;
        stat.text = "";
        description.text = "";
    }
    public void OnEquipButton()
    {
        var parent = GetComponentInParent<CharacterInfo>();
        var character = parent.curCharacter;
        if (itemData != null && itemData.owner != null)
        {
            alreadyEquip.Init(character, itemData, itemType);
            alreadyEquip.exit = parent.OnExitInventory;
            alreadyEquip.gameObject.SetActive(true);
            
        }
        else if(itemData != null)
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
}
