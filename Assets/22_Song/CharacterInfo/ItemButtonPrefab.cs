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
        weight.text = $"���� { itemdata.weight}";
        durability.text = $"������ {itemdata.durability}";
    }
}
