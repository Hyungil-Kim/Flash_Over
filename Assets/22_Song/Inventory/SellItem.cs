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

    //임시로 때려박아버리ㄱ기 나중에 수정하든가 합시다 ..
    public InventoryItemList inventoryItemList;
    //private ItemPrefab selectItem;
    private ItemDataBase selectItem;

    private void OnEnable()
    {
        inputField.text = "1";
        itemCount = int.Parse(inputField.text);
        //selectItem = inventoryItemList.SelectItem.GetComponent<ItemPrefab>();
        selectItem = inventoryItemList.curItem;
        //icon.sprite = selectItem.itemData.dataTable.iconSprite;
        icon.sprite = selectItem.dataTable.iconSprite;
    }

    public void OnPlus()
    {
        itemCount++;
        itemCount = Mathf.Clamp(itemCount, 1, selectItem.count);
        inputField.text = itemCount.ToString();
    }
    public void OnMinus()
    {
        itemCount--;
        itemCount = Mathf.Clamp(itemCount, 1, selectItem.count);
        inputField.text = itemCount.ToString();
    }
    public void OnSellButton()
    {
        var currentType = inventoryItemList.CurrentItemType;
        GameData.userData.RemoveItem(selectItem, currentType, itemCount);
        GameData.userData.gold += (int)(selectItem.dataTable.price * itemCount * 0.5f);
        inventoryItemList.Init();
        gameObject.SetActive(false);
    }
}
