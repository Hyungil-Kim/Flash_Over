using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI grade;
    public TextMeshProUGUI price;

    public void Init(ItemDataBase itemData)
    {
        icon.color = Color.white;
        icon.sprite = itemData.dataTable.iconSprite;
        grade.text = itemData.dataTable.grade;
        price.text = itemData.dataTable.price.ToString();
    }
    public void None()
    {
        icon.sprite = null;
        icon.color = Color.clear;
        grade.text = "";
        price.text = "";
    }
}
