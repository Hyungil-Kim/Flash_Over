using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemPrefab : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemStat;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI number;
    public ItemDataBase itemData;
    public InventoryItemList inventoryItemList;
    public void Init(ItemDataBase data, ItemType type)
    {
        itemData = data;

        number.text = itemData.count.ToString();
        image.sprite = itemData.dataTable.iconSprite;
        itemName.text = itemData.dataTable.itemName;
        switch (data.type)
        {
            case ItemType.Hose:
                var hose = (HoseData)data;
                itemStat.text = $"직사형 {hose.hoseData.dmg}\n분사형 {hose.hoseData.burnDmg}";
                break;
            case ItemType.BunkerGear:
                var bunkerGear = (BunkerGearData)data;
                itemStat.text = $"방어력 {bunkerGear.bunkerGearData.def}";
                break;
            case ItemType.OxygenTank:
                var oxygentank = (OxygenTankData)data;
                itemStat.text = $"충전량 {oxygentank.oxygenTankData.sta}";
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                break;
            default:
                break;
        }
        itemPrice.text = $"가격 {itemData.dataTable.price}";

        gameObject.SetActive(true);
    }
    public void Sell()
    {
        GetComponentInParent<InventoryItemList>().curItem = itemData;
        GetComponentInParent<ShopUI>().ItemSell();
    }
}
