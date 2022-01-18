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
    //private List<WeaponData> weaponItems = 
    //    new List<WeaponData>();
    //private List<ConsumableItemData> consumableItems = 
    //    new List<ConsumableItemData>();
    private int maxItem = GameData.userData.maxItem;


    public GameObject weightFull;
    public AlreadyEquip alreadyEquip;

    //private ItemType curType;
    //private ItemDataBase curData;
    private void OnEnable()
    {
        var type = GetComponentInParent<CharacterInfo>().currentItemType;
        Init(type);
    }


    private void Start()
    {
        
    }
    public void Init(ItemType type)
    {
        itemInfo.None(type);
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
        var hoses = GameData.userData.hoseList;
        var bunkerGears = GameData.userData.bunkerGearList;
        var oxygenTanks = GameData.userData.oxygenTankList;
        switch (type)
        {
            case ItemType.Hose:
                for (int i = 0; i < hoses.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
                    itemObject.Init(hoses[i],type);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.BunkerGear:
                for (int i = 0; i < bunkerGears.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
                    itemObject.Init(bunkerGears[i],type);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.OxygenTank:
                for (int i = 0; i < oxygenTanks.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
                    itemObject.Init(oxygenTanks[i], type);
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
    public void WeightFull()
    {
        weightFull.SetActive(true);
    }
    public void AlreadyEquip(CharacterData character, ItemDataBase itemData, ItemType itemType)
    {
        alreadyEquip.Init(character, itemData, itemType);
        alreadyEquip.exit = GetComponentInParent<CharacterInfo>().OnExitInventory;
        alreadyEquip.gameObject.SetActive(true);
    }
}
