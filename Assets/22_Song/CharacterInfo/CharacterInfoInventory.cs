using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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

    public int sortIndex;

    public GameObject weightFull;
    public GameObject alreadyEquip;

    public TMP_Dropdown optionDropdown;

    public ItemType type;
    public InfoType infoType;
    public bool isReverse;

    public ItemButtonPrefab hose;
    public ItemButtonPrefab bunkerGear;
    public ItemButtonPrefab oxygenTank;
    //private ItemType curType;
    //private ItemDataBase curData;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        CurrentItemType = GetComponentInParent<CharacterInfo>().currentItemType;
    }
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
            DropDownInit();
            Init();
        }
    }

    private void Start()
    {
        
    }
    public void Init()
    {
        CharacterData curCharacter = null;
        int curIndex = 0;
        switch (infoType)
        {
            case InfoType.Character:
                curCharacter = GetComponentInParent<CharacterInfo>().curCharacter;
                curIndex = GetComponentInParent<CharacterInfo>().currentIndex;
                break;
            case InfoType.Training:
                curCharacter = GetComponentInParent<TrainingCharacter>().curCharacter;
                curIndex = GetComponentInParent<TrainingCharacter>().curIndex;
                break;
            case InfoType.Rest:
                curCharacter = GetComponentInParent<Rest>().curCd;
                curIndex = GetComponentInParent<Rest>().curCharacterIndex;
                break;
            case InfoType.Truck:
                curCharacter = GetComponentInParent<FireTruck>().curcharacter;
                curIndex = GetComponentInParent<FireTruck>().curCharacterIndex;
                break;
            default:
                break;
        }

        if (curCharacter.hose != null)
        {
            var hoseData = curCharacter.hose.dataTable;
            hose.Init(hoseData);
            //var grade = GetGrade(hoseData);

            //hosestat.text = $"{hoseData.itemName}   {grade}\n성능 : {hoseData.dmg}\n무게 : {hoseData.weight}";
        }
        else
        {
            hose.icon.sprite = null;
            //hosestat.text = "";
        }
        if (curCharacter.bunkerGear != null)
        {
            var bunkerGearData = curCharacter.bunkerGear.dataTable;
            bunkerGear.Init(bunkerGearData);
            //var grade = GetGrade(bunkerGearData);
            //bunkergearstat.text = $"{bunkerGearData.itemName}  {grade}\n성능 : {bunkerGearData.def}\n무게 : {bunkerGearData.weight}";
        }
        else
        {
            bunkerGear.icon.sprite = null;
            //bunkergearstat.text = "";
        }
        if (curCharacter.oxygenTank != null)
        {
            var oxygenTankData = curCharacter.oxygenTank.dataTable;
            oxygenTank.Init(oxygenTankData);
            //oxygenTank.sprite = oxygenTankData.iconSprite;
            //var grade = GetGrade(oxygenTankData);
            //oxygenstat.text = $"{oxygenTankData.itemName}   {grade}\n성능 : {oxygenTankData.sta}\n무게 : {oxygenTankData.weight}";
        }
        else
        {
            oxygenTank.icon.sprite = null;
            //oxygenstat.text = "";
        }

        itemInfo.None(currentItemType);
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

        switch (currentItemType)
        {
            case ItemType.Hose:
                var hoseSort = (HoseOrder)sortIndex;
                var sortHoseItems = hoses;
                switch (hoseSort)
                {
                    case HoseOrder.Default:
                        sortHoseItems = hoses.Select((x) => x).ToList();
                        break;
                    case HoseOrder.Str:
                        sortHoseItems = hoses.OrderBy((x) => x.hoseData.dmg).ToList();
                        break;
                    case HoseOrder.Cost:
                        sortHoseItems = hoses.OrderBy((x) => x.hoseData.price).ToList();
                        break;
                    default:
                        break;
                }
                if (isReverse)
                {
                    sortHoseItems.Reverse();
                }
                for (int i = 0; i < sortHoseItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
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
                        sortBunkerGearItems = bunkerGears.Select((x)=>x).ToList();
                        break;
                    case BunkerGearOrder.Cost:
                        sortBunkerGearItems = bunkerGears.OrderBy((x) => x.bunkerGearData.price).ToList();
                        break;
                }
                if (isReverse)
                {
                    sortBunkerGearItems.Reverse();
                }
                for (int i = 0; i < sortBunkerGearItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
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
                        sortoxygenTankItems = oxygenTanks.Select((x) => x).ToList();
                        break;
                    case OxygenTankOrder.Cost:
                        sortoxygenTankItems = oxygenTanks.OrderBy((x) => x.oxygenTankData.price).ToList();
                        break;
                }
                if (isReverse)
                {
                    sortoxygenTankItems.Reverse();
                }
                for (int i = 0; i < sortoxygenTankItems.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
                    itemObject.Init(sortoxygenTankItems[i], ItemType.OxygenTank);
                    itemObjects[i].SetActive(true);
                }
                break;
            default:
                break;
        }

        //switch (type)
        //{
        //    case ItemType.Hose:
        //        for (int i = 0; i < hoses.Count; i++)
        //        {
        //            var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
        //            itemObject.Init(hoses[i],type);
        //            itemObjects[i].SetActive(true);
        //        }
        //        break;
        //    case ItemType.BunkerGear:
        //        for (int i = 0; i < bunkerGears.Count; i++)
        //        {
        //            var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
        //            itemObject.Init(bunkerGears[i],type);
        //            itemObjects[i].SetActive(true);
        //        }
        //        break;
        //    case ItemType.OxygenTank:
        //        for (int i = 0; i < oxygenTanks.Count; i++)
        //        {
        //            var itemObject = itemObjects[i].GetComponent<InfoInventoryItem>();
        //            itemObject.Init(oxygenTanks[i], type);
        //            itemObjects[i].SetActive(true);
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }
    public void DropDownInit()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        //var enumNames = System.Enum.GetNames(typeof(CharacterOrder));
        //foreach (var enumName in enumNames)
        //{
        //    options.Add(new TMP_Dropdown.OptionData(enumName));
        //}
        
        switch (CurrentItemType)
        {
            case ItemType.Hose:
                options.Add(new TMP_Dropdown.OptionData("획득순"));
                options.Add(new TMP_Dropdown.OptionData("무게"));
                options.Add(new TMP_Dropdown.OptionData("데미지"));
                options.Add(new TMP_Dropdown.OptionData("등급"));
                break;
            case ItemType.BunkerGear:
                options.Add(new TMP_Dropdown.OptionData("획득순"));
                options.Add(new TMP_Dropdown.OptionData("무게"));
                options.Add(new TMP_Dropdown.OptionData("방어구"));
                options.Add(new TMP_Dropdown.OptionData("등급"));
                break;
            case ItemType.OxygenTank:
                options.Add(new TMP_Dropdown.OptionData("획득순"));
                options.Add(new TMP_Dropdown.OptionData("무게"));
                options.Add(new TMP_Dropdown.OptionData("산소"));
                options.Add(new TMP_Dropdown.OptionData("등급"));
                break;
            case ItemType.Max:
                break;
            case ItemType.Consumable:
                break;
            default:
                break;
        }

        optionDropdown.options = options;
        sortIndex = 0;
        
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
        var already = alreadyEquip.GetComponentInChildren<AlreadyEquip>();
        already.Init(character, itemData, itemType);
        already.exit = GetComponentInParent<CharacterInfo>().OnExitInventory;
        alreadyEquip.SetActive(true);
    }
    public void OptionSort(int index)
    {
        sortIndex = index;
        Init();
    }
    public void OnItem(int index)
    {
        CurrentItemType = (ItemType)index;
        GetComponentInParent<CharacterInfo>().currentItemType = CurrentItemType;
    }
    public void SortReverse(bool check)
    {
        isReverse = check;
        Init();
    }
}
