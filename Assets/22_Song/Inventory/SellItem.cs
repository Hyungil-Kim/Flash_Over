using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellItem : MonoBehaviour
{
    private int itemCount = 1;
    public Image icon;
    public TMP_InputField inputField;

    //�ӽ÷� �����ھƹ������� ���߿� �����ϵ簡 �սô� ..
    public InventoryItemList inventoryItemList;
    private ItemPrefab selectItem;

    private void OnEnable()
    {
        inputField.text = "1";
        itemCount = int.Parse(inputField.text);
        selectItem = inventoryItemList.SelectItem.GetComponent<ItemPrefab>();
        icon.sprite = selectItem.itemData.dataTable.iconSprite;
    }

    public void OnPlus()
    {
        itemCount++;
        itemCount = Mathf.Clamp(itemCount, 1, selectItem.itemData.count);
        inputField.text = itemCount.ToString();
    }
    public void OnMinus()
    {
        itemCount--;
        itemCount = Mathf.Clamp(itemCount, 1, selectItem.itemData.count);
        inputField.text = itemCount.ToString();
    }
    public void OnSellButton()
    {
        var currentType = inventoryItemList.CurrentItemType;
        GameData.userData.RemoveItem(selectItem.itemData, currentType, itemCount);
        GameData.userData.gold += (int)(selectItem.itemData.dataTable.price * itemCount * 0.5f);
        inventoryItemList.Init();
        gameObject.SetActive(false);
    }
}