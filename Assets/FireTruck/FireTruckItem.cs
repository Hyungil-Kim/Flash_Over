using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireTruckItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;


    public void Init(ItemDataBase item)
    {
        icon.sprite = item.dataTable.iconSprite;
        itemName.text = item.dataTable.itemName;
    }
}
