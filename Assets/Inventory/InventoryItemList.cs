using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemList : MonoBehaviour
{
    public int maxItem;

    public GameObject itemPrefab;
    public GameObject content;
    private GameObject selectItem;
    public GameObject SelectItem
    {
        get
        {
            return selectItem;
        }
    }

    List<GameObject> itemObjects = new List<GameObject>();

    List<WeaponData> weaponItems
        = new List<WeaponData>();
    List<ConsumableItemData> consumableItems
        = new List<ConsumableItemData>();

    private ItemType currentItemType = ItemType.Consumable;
    public ItemType CurrentItemType
    {
        get
        {
            return currentItemType;
        }
        set
        {
            currentItemType = value;
            Init(value);
        }
    }

    private void Start()
    {
        //맥스 슬롯만큼 프리펩아이템 오브젝트 생성
        for (int i = 0; i < maxItem; i++)
        {
            var go = Instantiate(itemPrefab, content.transform);
            go.SetActive(false);
            itemObjects.Add(go);
            var button = go.GetComponent<Button>();
            button.onClick.AddListener(() => SetSelectItem(go));
        }
        CurrentItemType = currentItemType;
    }
    public void Init(ItemType type)
    {
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
                    var itemObject = itemObjects[i].GetComponent<ItemPrefab>();
                    itemObject.Init(consumableItems[i], ItemType.Consumable);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.Weapon:
                for (int i = 0; i < weaponItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<ItemPrefab>();
                    itemObject.Init(weaponItems[i], ItemType.Weapon);
                    itemObjects[i].SetActive(true);
                }
                break;
            default:
                break;
        }
    }
    public void SetSelectItem(GameObject go)
    {
        selectItem = go;
    }

}
