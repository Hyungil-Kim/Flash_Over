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

    public void Init(ItemDataBase data, ItemType type)
    {
        itemData = data;
        switch (type)
        {
            case ItemType.Consumable:
                var consumableData = itemData as ConsumableItemData;
                number.text = consumableData.count.ToString();
                break;
            case ItemType.Weapon:
                var weaponData = itemData as WeaponData;
                number.text = weaponData.count.ToString();
                break;
            default:
                break;
        }
        image.sprite = itemData.dataTable.iconSprite;
        itemName.text = itemData.dataTable.itemName;
        gameObject.SetActive(true);
    }
}
