using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class ShopUpgradeData
{

}
public class Shop : MonoBehaviour
{
    public int maxItemCount;
    public float initSecondTime;
    public ShopItemInfo info;
    public GameObject shopItemPrefab;
    public GameObject content;
    public GameObject notEnoughGold;
    List<GameObject> shopItemList = new List<GameObject>();

    public int hoseItemCount = 2;
    public int bunkerGearItemCount = 2;
    public int oxygenTankItemCount = 2;

    public TextMeshProUGUI gold;
    private void Awake()
    {
        ////�켱�����ϸ� �� �������������� �ƽ������۰�����ŭ ������ٰſ���
        //for (int i = 0; i < maxItemCount; i++)
        //{
        //    var index = i;
        //    var shopItem = Instantiate(shopItemPrefab, content.transform);
        //    var shopItemScript = shopItem.GetComponent<ShopItem>();
        //    shopItemScript.buyButton.onClick.AddListener(() => OnBuyButton(index));
        //    shopItemScript.backGroundButton.onClick.AddListener(() => OnItemInfo(index));
        //    shopItemList.Add(shopItem);
        //}
    }
    public void Init()
    {
        if (shopItemList.Count == 0)
        {
            //�켱�����ϸ� �� �������������� �ƽ������۰�����ŭ ������ٰſ���
            for (int i = 0; i < hoseItemCount + bunkerGearItemCount + oxygenTankItemCount; i++)
            {
                var index = i;
                var shopItem = Instantiate(shopItemPrefab, content.transform);
                var shopItemScript = shopItem.GetComponent<ShopItem>();
                shopItemScript.buyButton.onClick.AddListener(() => OnBuyButton(index));
                shopItemScript.backGroundButton.onClick.AddListener(() => OnItemInfo(index));
                shopItemList.Add(shopItem);
            }
        }
    }
    private void OnEnable()
    {
        Init();
        //�ε� �� ������������ ������ ��������� ������Ʈ���ٰſ���
        if (GameData.userData.shopItemList.Count == 0)
        {
            ShopUpdate();
        }
        else
        {
            //�ƴ϶�� ����� ���������� ����Ʈ�� �ҷ��ͼ� �������̴ٰϴ�
            for (int i = 0; i < maxItemCount; i++)
            {
                SetList(GameData.userData.shopItemList[i], i);
            }
        }
    }

    private void Start()
    {
        //�����ð����� �Ҷ� �ʿ�

        //GameData.shopData.LoadShop();
        //if(GameData.shopData.shopTime == default)
        //{
        //    GameData.shopData.shopTime = System.DateTime.Now;
        //}

        //���� maxItemcou
        //for (int i = 0; i < maxItemCount; i++)
        //{
        //    var index = i;
        //    var shopItem = Instantiate(shopItemPrefab, content.transform);
        //    var shopItemScript = shopItem.GetComponent<ShopItem>();
        //    shopItemScript.buyButton.onClick.AddListener(() => OnBuyButton(index));
        //    shopItemList.Add(shopItem);
        //}

        //if(GameData.userData.shopItemList.Count == 0)
        //{
        //    ShopListUpdate();
        //}
        //else
        //{
        //    for (int i = 0; i < maxItemCount; i++)
        //    {
        //        SetList(GameData.userData.shopItemList[i], i);
        //    }
        //}
    }
    private void Update()
    {
        //���� �ð� �����ؼ� ����Ʈ �ֽ�ȭ

        //var nowTime = System.DateTime.Now;
        //var intervalTime = nowTime - GameData.shopData.shopTime;
        //if (intervalTime.TotalSeconds >= initSecondTime)
        //{
        //    GameData.shopData.shopTime = nowTime;
        //    ShopListUpdate();
        //}
        gold.text = $"Gold : {GameData.userData.gold.ToString("D5")}";


        //test��
        //// �ڵ��������� ��ü�ؾ߰���
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    GameData.userData.SaveUserData(1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.Log(GameData.userData.shopTime);
        //}
        ////������Ʈ ������Ʈ �Ǵ� �κ� ���� ���߿� �����ؾ߰���
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ShopListUpdate();
        //}
    }
    public void ShopUpdate()
    {
        ////�����۸���Ʈ�� ����
        //GameData.userData.shopItemList.Clear();
        //for (int i = 0; i < maxItemCount; i++)
        //{
        //    //����ġ ��� �̱�
        //    var gradeWeight = MyDataTableMgr.itemGradeTable.GetTable(GameData.userData.shopLevel);
        //    var itemGrade = new WeightList<ItemGrade>();
        //    itemGrade.AddGrade(ItemGrade.Normal, gradeWeight.normal);
        //    itemGrade.AddGrade(ItemGrade.Rare, gradeWeight.rare);
        //    itemGrade.AddGrade(ItemGrade.Unique, gradeWeight.unique);
        //    itemGrade.AddGrade(ItemGrade.Special, gradeWeight.special);

        //    //��޺� ������ �̱� !
        //    var grade = itemGrade.GetRandomGrade();
        //    var randomType = (ItemType)Random.Range(0, (int)ItemType.Max);
        //    var gradeItem = GetRandomItem(grade, randomType);


        //    //���� ���������۸���Ʈ �߰�
        //    GameData.userData.shopItemList.Add(gradeItem);
        //    SetList(gradeItem, i);

        //    GameData.userData.SaveUserData(1);
        //}
        GameData.userData.shopItemList.Clear();


        RandomItem(ItemType.Hose, 
            0, hoseItemCount);
        RandomItem(ItemType.BunkerGear, 
            hoseItemCount, bunkerGearItemCount);
        RandomItem(ItemType.OxygenTank, 
            hoseItemCount + bunkerGearItemCount, oxygenTankItemCount);
        
    }
    public void RandomItem(ItemType type, int startIndex, int endIndex)
    {
        for (int i = startIndex; i < startIndex+endIndex; i++)
        {
            var random = 0;
            ItemDataBase item = null;
            ItemTableDataBase itemTable = null;
            var index = i;
            switch (type)
            {
                case ItemType.Hose:
                    random = Random.Range(0, MyDataTableMgr.hoseTable.tables.Count);
                    itemTable = MyDataTableMgr.hoseTable.tables[random];
                    item = new HoseData(itemTable);
                    break;
                case ItemType.BunkerGear:
                    random = Random.Range(0, MyDataTableMgr.bunkerGearTable.tables.Count);
                    itemTable = MyDataTableMgr.bunkerGearTable.tables[random];
                    item = new BunkerGearData(itemTable);
                    break;
                case ItemType.OxygenTank:
                    random = Random.Range(0, MyDataTableMgr.oxygenTankTable.tables.Count);
                    itemTable = MyDataTableMgr.oxygenTankTable.tables[random];
                    item = new OxygenTankData(itemTable);
                    break;
                default:
                    break;
            }
            GameData.userData.shopItemList.Add(item);
            SetList(item, index);
        }
    }
    //public ItemDataBase GetRandomConsumItem(ItemGrade grade)
    //{
    //    //�Ҹ�ǰ�� ��� ������ �ȳ��´ٰ� ������...
    //    //�����а� ������ �� ������ 
    //    int itemCount = 3;
    //    switch (grade)
    //    {
    //        case ItemGrade.Normal:
    //            var randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.normalItem.Count);
    //            var dataTable = MyDataTableMgr.consumItemTable.normalItem[randomIndex];
    //            var itemGrade = StringToEnum.SToE<ItemGrade>(dataTable.grade);
    //            var count = ((int)ItemGrade.Normal - (int)itemGrade + 1) * itemCount;

    //            var item = new ConsumableItemData(dataTable,count);
    //            return item;
    //        case ItemGrade.Rare:
    //            randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.rareItem.Count);
    //            dataTable = MyDataTableMgr.consumItemTable.rareItem[randomIndex];
    //            itemGrade = StringToEnum.SToE<ItemGrade>(dataTable.grade);
    //            count = ((int)ItemGrade.Rare - (int)itemGrade + 1) * itemCount;

    //            item = new ConsumableItemData(dataTable, count);
    //            return item;
    //        case ItemGrade.Unique:
    //            randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.uniqueItem.Count);
    //            dataTable = MyDataTableMgr.consumItemTable.uniqueItem[randomIndex];
    //            itemGrade = StringToEnum.SToE<ItemGrade>(dataTable.grade);
    //            count = ((int)ItemGrade.Unique - (int)itemGrade + 1) * itemCount;

    //            item = new ConsumableItemData(dataTable, count);
    //            return item;
    //        case ItemGrade.Special:
    //            randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.specialItem.Count);
    //            dataTable = MyDataTableMgr.consumItemTable.specialItem[randomIndex];
    //            itemGrade = StringToEnum.SToE<ItemGrade>(dataTable.grade);
    //            count = ((int)ItemGrade.Special - (int)itemGrade + 1) * itemCount;

    //            item = new ConsumableItemData(dataTable, count);
    //            return item;
    //        default:
    //            return null;
    //    }
    //}
    public ItemDataBase GetRandomItem(ItemGrade grade, ItemType itemType)
    {
        //���� ȣ���� �����̴ϴ�.
        //int randomIndex = 0;
        ItemDataBase item;
        int randomIndex;
        ItemTableDataBase itemTable;

        switch (grade)
        {
            case ItemGrade.Normal:
                switch (itemType)
                {
                    case ItemType.Hose:
                        randomIndex = Random.Range(0, MyDataTableMgr.hoseTable.normalItem.Count);
                        itemTable = MyDataTableMgr.hoseTable.normalItem[randomIndex];
                        item = new HoseData(itemTable);
                        return item;
                    case ItemType.BunkerGear:
                        randomIndex = Random.Range(0, MyDataTableMgr.bunkerGearTable.normalItem.Count);
                        itemTable = MyDataTableMgr.bunkerGearTable.normalItem[randomIndex];
                        item = new BunkerGearData(itemTable);
                        return item;
                    case ItemType.OxygenTank:
                        randomIndex = Random.Range(0, MyDataTableMgr.oxygenTankTable.normalItem.Count);
                        itemTable = MyDataTableMgr.oxygenTankTable.normalItem[randomIndex];
                        item = new OxygenTankData(itemTable);
                        return item;
                }
                break;
            case ItemGrade.Rare:
                switch (itemType)
                {
                    case ItemType.Hose:
                        randomIndex = Random.Range(0, MyDataTableMgr.hoseTable.rareItem.Count);
                        itemTable = MyDataTableMgr.hoseTable.rareItem[randomIndex];
                        item = new HoseData(itemTable);
                        return item;
                    case ItemType.BunkerGear:
                        randomIndex = Random.Range(0, MyDataTableMgr.bunkerGearTable.rareItem.Count);
                        itemTable = MyDataTableMgr.bunkerGearTable.rareItem[randomIndex];
                        item = new BunkerGearData(itemTable);
                        return item;
                    case ItemType.OxygenTank:
                        randomIndex = Random.Range(0, MyDataTableMgr.oxygenTankTable.rareItem.Count);
                        itemTable = MyDataTableMgr.oxygenTankTable.rareItem[randomIndex];
                        item = new OxygenTankData(itemTable);
                        return item;
                }
                break;
            case ItemGrade.Unique:
                switch (itemType)
                {
                    case ItemType.Hose:
                        randomIndex = Random.Range(0, MyDataTableMgr.hoseTable.uniqueItem.Count);
                        itemTable = MyDataTableMgr.hoseTable.uniqueItem[randomIndex];
                        item = new HoseData(itemTable);
                        return item;
                    case ItemType.BunkerGear:
                        randomIndex = Random.Range(0, MyDataTableMgr.bunkerGearTable.uniqueItem.Count);
                        itemTable = MyDataTableMgr.bunkerGearTable.uniqueItem[randomIndex];
                        item = new BunkerGearData(itemTable);
                        return item;
                    case ItemType.OxygenTank:
                        randomIndex = Random.Range(0, MyDataTableMgr.oxygenTankTable.uniqueItem.Count);
                        itemTable = MyDataTableMgr.oxygenTankTable.uniqueItem[randomIndex];
                        item = new OxygenTankData(itemTable);
                        return item;
                }
                break;
            case ItemGrade.Special:
                switch (itemType)
                {
                    case ItemType.Hose:
                        randomIndex = Random.Range(0, MyDataTableMgr.hoseTable.specialItem.Count);
                        itemTable = MyDataTableMgr.hoseTable.specialItem[randomIndex];
                        item = new HoseData(itemTable);
                        return item;
                    case ItemType.BunkerGear:
                        randomIndex = Random.Range(0, MyDataTableMgr.bunkerGearTable.specialItem.Count);
                        itemTable = MyDataTableMgr.bunkerGearTable.specialItem[randomIndex];
                        item = new BunkerGearData(itemTable);
                        return item;
                    case ItemType.OxygenTank:
                        randomIndex = Random.Range(0, MyDataTableMgr.oxygenTankTable.specialItem.Count);
                        itemTable = MyDataTableMgr.oxygenTankTable.specialItem[randomIndex];
                        item = new OxygenTankData(itemTable);
                        return item;
                }
                break;
        }
        return null;
    }
    public void SetList(ItemDataBase itemDataBase, int index)
    {
        //�������� �������̴ٰϴ�.
        var shopPrefab = shopItemList[index].GetComponent<ShopItem>();

        shopPrefab.Init(itemDataBase);
        if(itemDataBase.isSold)
        {
            shopPrefab.Sold();
        }
    }
    public void OnBuyButton(int index)
    {
        var shopItem = GameData.userData.shopItemList[index];
        var cheak = GameData.userData.shopItemList.Count < GameData.userData.maxItem;
        //�ִ� ������ ������ �Ѿ�� ���ϰ� �����ݴϴ�.
        if (!cheak)
        {
            return;
        }
        //���Ÿ� �Ұǵ� ���� �����ϸ� ���� �����մϴٸ� ���� �������̴ٰϴ�.
        if (shopItem.dataTable.price > GameData.userData.gold)
        {
            notEnoughGold.SetActive(true);
            return;
        }

        //���Ÿ� �Ұſ��� as �� �̿��Ͽ� �������� � ���������� �����Ͽ� ������ AddItem �� ����Ͽ� �ݴϴ�.
        GameData.userData.gold -= shopItem.dataTable.price;
        shopItem.isSold = true;
        var isHose = GameData.userData.shopItemList[index] as HoseData;
        var isBunkerGear = GameData.userData.shopItemList[index] as BunkerGearData;
        var isOxygenTank = GameData.userData.shopItemList[index] as OxygenTankData;
        if(isHose != null)
        {
            GameData.userData.AddItem(isHose.dataTable.id, ItemType.Hose, isHose.count);

            //test
            GameData.userData.SaveUserData(1);
        }
        else if(isBunkerGear != null)
        {
            GameData.userData.AddItem(isBunkerGear.dataTable.id, ItemType.BunkerGear, isBunkerGear.count);

            //test
            GameData.userData.SaveUserData(1);
        }
        else if (isOxygenTank != null)
        {
            GameData.userData.AddItem(isOxygenTank.dataTable.id, ItemType.OxygenTank, isOxygenTank.count);

            //test
            GameData.userData.SaveUserData(1);
        }
        //���Ÿ� �Ϸ��ϸ� sold out�� ����̴ٰϴ�.
        var shopPrefab = shopItemList[index].GetComponent<ShopItem>();
        shopPrefab.Sold();
    }

    public void OnShopLevelUpButton()
    {
        //���������� 
        //GameData.userData.shopLevel = Mathf.Clamp(GameData.userData.shopLevel + 1, 0, MyDataTableMgr.itemGradeTable.tables.Count-1);
        if(GameData.userData.shopLevel < MyDataTableMgr.itemGradeTable.tables.Count - 1)
        {
            GameData.userData.shopLevel += 1;
            ShopUpdate();
        }
    }
    public void OnNotEnoughtOkay()
    {
        notEnoughGold.SetActive(false);
    }
    public void OnItemInfo(int index)
    {
        info.gameObject.SetActive(true);
        info.Init(index);
    }
    //test
    public void GetGold()
    {
        GameData.userData.gold += 100;
    }
}
