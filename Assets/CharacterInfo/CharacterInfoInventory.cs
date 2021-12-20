using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoInventory : MonoBehaviour
{
    public ItemInfo itemInfo;
    public GameObject content;
    public GameObject itemPrefab;
    public List<GameObject> itemObjects = 
        new List<GameObject>();
    private List<WeaponData> weaponItems = 
        new List<WeaponData>();
    private List<ConsumableItemData> consumableItems = 
        new List<ConsumableItemData>();
    public int maxItem = 50;

    private void Start()
    {

    }
    public void Init(ItemType type)
    {
        if(itemObjects.Count == 0)
        {
            for (int i = 0; i < maxItem; i++)
            {
                var item = Instantiate(itemPrefab, content.transform);
                var button = item.GetComponent<Button>();
                var itemObject = item.GetComponent<InfoInventoryItem>();
                button.onClick.AddListener(() => ItemInit(itemObject.itemData,itemObject.itemType));
                item.SetActive(false);
                itemObjects.Add(item);
            }
        }
        foreach (var item in itemObjects)
        {
            item.SetActive(false);
        }
        consumableItems = GameData.userData.consumableItemList;
        weaponItems = GameData.userData.weaponItemList;
        switch (type)
        {
            case ItemType.Consumable:
                for (int i = 0; i < consumableItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
                    itemObject.Init(consumableItems[i],type);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.Weapon:
                for (int i = 0; i < weaponItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
                    itemObject.Init(weaponItems[i],type);
                    itemObjects[i].SetActive(true);
                }
                break;
            default:
                break;
        }
    }
    public void ItemInit(ItemDataBase data, ItemType type)
    {
        itemInfo.Init(data, type);
    }
}
