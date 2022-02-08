using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButtonPrefab : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI durability;
    public void Init(ItemTableDataBase itemdata)
    {
        icon.sprite = itemdata.iconSprite;
        itemName.text = itemdata.itemName;
        weight.text = $"무게 { itemdata.weight}";
        durability.text = $"내구도 {itemdata.durability}";
    }
}
