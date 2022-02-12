using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI price;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI count;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI soldOut;
    public Button buyButton;
    public Button backGroundButton;
    
    public void Init(ItemDataBase itemData)
    {
        backGroundButton.interactable = true;
        soldOut.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(true);
        image.sprite = itemData.dataTable.iconSprite;
        price.text = $"{itemData.dataTable.price}골드";
        itemName.text = itemData.dataTable.itemName;
        count.text = $"내구도:{ itemData.dataTable.durability.ToString("D3")}";
        switch (itemData.type)
        {
            case ItemType.Hose:
                stat.text = $"공격력:{itemData.dataTable.dmg}";
                break;
            case ItemType.BunkerGear:
                stat.text = $"방어력:{itemData.dataTable.def}";
                break;
            case ItemType.OxygenTank:
                stat.text = $"산소:{itemData.dataTable.sta}";
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                break;
            default:
                break;
        }
        
    }
    public void Sold()
    {
        soldOut.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(false);
        backGroundButton.interactable = false;
    }
}
