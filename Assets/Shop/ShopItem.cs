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
    public TextMeshProUGUI soldOut;
    public Button buyButton;
    public Button backGroundButton;
    
    public void Init(ItemDataBase itemData)
    {
        backGroundButton.interactable = true;
        soldOut.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(true);
        image.sprite = itemData.dataTable.iconSprite;
        price.text = $"{itemData.dataTable.price}";
        itemName.text = itemData.dataTable.itemName;
        count.text = itemData.count.ToString("D3");
    }
    public void Sold()
    {
        soldOut.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(false);
        backGroundButton.interactable = false;
    }
}
