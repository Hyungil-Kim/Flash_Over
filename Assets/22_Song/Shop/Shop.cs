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
        ////우선실행하면 빈 아이템프리펩을 맥스아이템갯수만큼 만들어줄거예요
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
            //우선실행하면 빈 아이템프리펩을 맥스아이템갯수만큼 만들어줄거예요
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
        //로드 된 상점아이템이 없으면 상점목록을 업데이트해줄거예요
        if (GameData.userData.shopItemList.Count == 0)
        {
            ShopUpdate();
        }
        else
        {
            //아니라면 저장된 상점아이템 리스트를 불러와서 세팅해줄겁니다
            for (int i = 0; i < maxItemCount; i++)
            {
                SetList(GameData.userData.shopItemList[i], i);
            }
        }
    }

    private void Start()
    {
        //실제시간으로 할때 필요

        //GameData.shopData.LoadShop();
        //if(GameData.shopData.shopTime == default)
        //{
        //    GameData.shopData.shopTime = System.DateTime.Now;
        //}

        //지금 maxItemcou
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
        //실제 시간 저장해서 리스트 최신화

        //var nowTime = System.DateTime.Now;
        //var intervalTime = nowTime - GameData.shopData.shopTime;
        //if (intervalTime.TotalSeconds >= initSecondTime)
        //{
        //    GameData.shopData.shopTime = nowTime;
        //    ShopListUpdate();
        //}
        gold.text = $"Gold : {GameData.userData.gold.ToString("D5")}";


        //test용
        //// 자동저장으로 대체해야겠지
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    GameData.userData.SaveUserData(1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.Log(GameData.userData.shopTime);
        //}
        ////샵리스트 업데이트 되는 부분 조건 나중에 수정해야겠찌
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ShopListUpdate();
        //}
    }
    public void ShopUpdate()
    {
        ////아이템리스트를 비우고
        //GameData.userData.shopItemList.Clear();
        //for (int i = 0; i < maxItemCount; i++)
        //{
        //    //가중치 등급 뽑기
        //    var gradeWeight = MyDataTableMgr.itemGradeTable.GetTable(GameData.userData.shopLevel);
        //    var itemGrade = new WeightList<ItemGrade>();
        //    itemGrade.AddGrade(ItemGrade.Normal, gradeWeight.normal);
        //    itemGrade.AddGrade(ItemGrade.Rare, gradeWeight.rare);
        //    itemGrade.AddGrade(ItemGrade.Unique, gradeWeight.unique);
        //    itemGrade.AddGrade(ItemGrade.Special, gradeWeight.special);

        //    //등급별 아이템 뽑기 !
        //    var grade = itemGrade.GetRandomGrade();
        //    var randomType = (ItemType)Random.Range(0, (int)ItemType.Max);
        //    var gradeItem = GetRandomItem(grade, randomType);


        //    //현재 상점아이템리스트 추가
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
    //    //소모품은 비록 상점에 안나온다고 하지만...
    //    //만들어둔거 포기할 수 없었던 
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
        //랜덤 호스를 뽑을겁니다.
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
        //프리펩을 설정해줄겁니다.
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
        //최대 아이템 갯수를 넘어서지 못하게 막아줍니다.
        if (!cheak)
        {
            return;
        }
        //구매를 할건데 돈이 부족하면 돈이 부족합니다를 띄우고 리턴해줄겁니다.
        if (shopItem.dataTable.price > GameData.userData.gold)
        {
            notEnoughGold.SetActive(true);
            return;
        }

        //구매를 할거예요 as 를 이용하여 아이템이 어떤 아이템인지 구분하여 만들어둔 AddItem 을 사용하여 줍니다.
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
        //구매를 완료하면 sold out을 띄워줄겁니다.
        var shopPrefab = shopItemList[index].GetComponent<ShopItem>();
        shopPrefab.Sold();
    }

    public void OnShopLevelUpButton()
    {
        //상점레벨업 
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
