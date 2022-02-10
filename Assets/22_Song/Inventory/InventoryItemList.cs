using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryItemList : MonoBehaviour
{
    public int maxItem;
    private int sortIndex;
    public TMP_Dropdown optionDropdown;
    public InventoryItemInfo info;
    public GameObject itemPrefab;
    public GameObject content;
    private GameObject selectItem;
    private bool isReverse;
    public GameObject SelectItem
    {
        get
        {
            return selectItem;
        }
    }

    public ItemDataBase curItem;

    List<GameObject> itemObjects = new List<GameObject>();

    //List<WeaponData> weaponItems
    //    = new List<WeaponData>();
    //List<ConsumableItemData> consumableItems
    //    = new List<ConsumableItemData>();

    private ItemType currentItemType = ItemType.Hose;
    public ItemType CurrentItemType
    {
        get
        {
            return currentItemType;
        }
        set
        {
            // 아이템 분류 누를때마다 커렌트 타입바꾸면서 한번만 Init하게끔
            currentItemType = value;
            sortIndex = 0;
            optionDropdown.value = 0;
            Init();
            DropDownInit();
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
    //아이템 리스트를 해당하는 타입으로 나오게
    public void Init()
    {
        foreach (var item in itemObjects)
        {
            item.SetActive(false);
        }
        var hoses = GameData.userData.hoseList;
        var bunkerGears = GameData.userData.bunkerGearList;
        var oxygenTanks = GameData.userData.oxygenTankList;
        switch (currentItemType)
        {
            case ItemType.Hose:
                var hoseSort = (HoseOrder)sortIndex;
                var sortHoseItems = hoses;
                switch (hoseSort)
                {
                    case HoseOrder.Default:
                        sortHoseItems = hoses.Where((x) => x.owner == null).Select((x) => x).ToList();
                        break;
                    case HoseOrder.Str:
                        sortHoseItems = hoses.Where((x) => x.owner == null).OrderBy((x) => x.hoseData.dmg).ToList();
                        break;
                    case HoseOrder.Cost:
                        sortHoseItems = hoses.Where((x) => x.owner == null).OrderBy((x) => x.hoseData.price).ToList();
                        break;
                    default:
                        break;
                }
                if(isReverse)
                {
                    sortHoseItems.Reverse();
                }
                for (int i = 0; i < sortHoseItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<ItemPrefab>();
                    itemObject.Init(sortHoseItems[i], ItemType.Hose);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.BunkerGear:
                var bunkerGearSort = (BunkerGearOrder)sortIndex;
                var sortBunkerGearItems = bunkerGears;
                switch (bunkerGearSort)
                {
                    case BunkerGearOrder.Default:
                        sortBunkerGearItems = bunkerGears.Where((x) => x.owner == null).ToList();
                        break;
                    case BunkerGearOrder.Cost:
                        sortBunkerGearItems = bunkerGears.Where((x) => x.owner == null).OrderBy((x) => x.bunkerGearData.price).ToList();
                        break;
                }
                if (isReverse)
                {
                    sortBunkerGearItems.Reverse();
                }
                for (int i = 0; i < sortBunkerGearItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<ItemPrefab>();
                    itemObject.Init(sortBunkerGearItems[i], ItemType.BunkerGear);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.OxygenTank:
                var oxygenTankSort = (OxygenTankOrder)sortIndex;
                var sortoxygenTankItems = oxygenTanks;
                switch (oxygenTankSort)
                {
                    case OxygenTankOrder.Default:
                        sortoxygenTankItems = oxygenTanks.Where((x) => x.owner == null).Select((x) => x).ToList();
                        break;
                    case OxygenTankOrder.Cost:
                        sortoxygenTankItems = oxygenTanks.Where((x) => x.owner == null).OrderBy((x) => x.oxygenTankData.price).ToList();
                        break;
                }
                if (isReverse)
                {
                    sortoxygenTankItems.Reverse();
                }
                for (int i = 0; i < sortoxygenTankItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<ItemPrefab>();
                    itemObject.Init(sortoxygenTankItems[i], ItemType.OxygenTank);
                    itemObjects[i].SetActive(true);
                }
                break;
            default:
                break;
        }
        if (itemObjects[0].activeSelf)
        {
            SetSelectItem(itemObjects[0]);
        }
        else
        {
            info.None();
        }
    }
    public void DropDownInit()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        switch (currentItemType)
        {
            case ItemType.Hose:
                var enumNames = System.Enum.GetNames(typeof(HoseOrder));
                foreach (var enumName in enumNames)
                {
                    options.Add(new TMP_Dropdown.OptionData(enumName));
                }
                break;
            case ItemType.BunkerGear:
                enumNames = System.Enum.GetNames(typeof(BunkerGearOrder));
                foreach (var enumName in enumNames)
                {
                    options.Add(new TMP_Dropdown.OptionData(enumName));
                }
                break;
            case ItemType.OxygenTank:
                enumNames = System.Enum.GetNames(typeof(OxygenTankOrder));
                foreach (var enumName in enumNames)
                {
                    options.Add(new TMP_Dropdown.OptionData(enumName));
                }
                break;
            default:
                break;
        }
        optionDropdown.options = options;
    }
    public void OptionSort(int index)
    {
        sortIndex = index;
        Init();
    }
    public void SortReverse(bool istrue)
    {
        isReverse = istrue;
        Init();
    }
    // 아이템클릭했을때, 클릭한 아이템을 현재 선택 아이템으로 설정
    public void SetSelectItem(GameObject go)
    {
        selectItem = go;
        var itemObject = go.GetComponent<ItemPrefab>();
        info.Init(itemObject.itemData);
    }
    public void OnItem(int index)
    {
        CurrentItemType = (ItemType)index;
    }

}
