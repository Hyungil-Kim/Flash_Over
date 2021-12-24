using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoInventoryItem : MonoBehaviour
{
    public ItemDataBase itemData;
    public GameObject equipImage;
    public ItemType itemType;
    public void Init(ItemDataBase data ,ItemType type)
    {
        itemData = data;
        itemType = type;
        var icon = GetComponent<Image>();
        icon.sprite = itemData.dataTable.iconSprite;

        equipImage.SetActive(false);
        if (itemData.owner != null)
        {
            equipImage.SetActive(true);
        }
    }
    //public void OnItemIcon()
    //{
    //    //좀 이상함
    //    var parent =GetComponentInParent<CharacterInfoInventory>();
    //    parent.itemInfo.Init(itemData, itemType);
    //}
}
