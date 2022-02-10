using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemPrefab : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI number;
    public ItemDataBase itemData;
    public InventoryItemList inventoryItemList;
    public void Init(ItemDataBase data, ItemType type)
    {
        itemData = data;

        number.text = itemData.count.ToString();
        image.sprite = itemData.dataTable.iconSprite;
        itemName.text = itemData.dataTable.itemName;
        gameObject.SetActive(true);
    }
    public void Sell()
    {
        GetComponentInParent<InventoryItemList>().curItem = itemData;
        GetComponentInParent<ShopUI>().ItemSell();
    }
}
