using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ConsumShop : MonoBehaviour
{
    //public int maxItemCount;
    //public float initSecondTime;
    //public ShopItemInfo info;
    //public GameObject shopItemPrefab;
    //public GameObject content;
    //public GameObject notEnoughGold;
    //List<GameObject> shopItemList = new List<GameObject>();

    //public TextMeshProUGUI gold;
    //private void Awake()
    //{
    //    //우선실행하면 빈 아이템프리펩을 맥스아이템갯수만큼 만들어줄거예요
    //    for (int i = 0; i < maxItemCount; i++)
    //    {
    //        var index = i;
    //        var shopItem = Instantiate(shopItemPrefab, content.transform);
    //        var shopItemScript = shopItem.GetComponent<ShopItem>();
    //        shopItemScript.buyButton.onClick.AddListener(() => OnBuyButton(index));
    //        shopItemScript.backGroundButton.onClick.AddListener(() => OnItemInfo(index));
    //        shopItemList.Add(shopItem);
    //    }
    //}
    //private void OnEnable()
    //{
    //    //로드 된 상점아이템이 없으면 상점목록을 업데이트해줄거예요
    //    if (GameData.userData.shopItemList.Count == 0)
    //    {
    //        ShopUpdate();
    //    }
    //}
    //private void Start()
    //{
    //}
    //private void Update()
    //{
    //    gold.text = $"Gold : {GameData.userData.gold.ToString("D5")}";
    //}
    //public void ShopUpdate()
    //{
    //    //아이템리스트를 비우고
    //    GameData.userData.shopItemList.Clear();
    //    for (int i = 0; i < maxItemCount; i++)
    //    {
    //        //가중치 등급 뽑기
    //        var gradeWeight = MyDataTableMgr.itemGradeTable.GetTable(GameData.userData.shopLevel);
    //        var itemGrade = new WeightList<ItemGrade>();
    //        itemGrade.AddGrade(ItemGrade.Normal, gradeWeight.normal);
    //        itemGrade.AddGrade(ItemGrade.Rare, gradeWeight.rare);
    //        itemGrade.AddGrade(ItemGrade.Unique, gradeWeight.unique);
    //        itemGrade.AddGrade(ItemGrade.Special, gradeWeight.special);

    //        //등급별 아이템 뽑기 !
    //        var grade = itemGrade.GetRandomGrade();
    //        var gradeItem = GetRandomConsumItem(grade);

    //        //현재 상점아이템리스트 추가
    //        SetList(gradeItem, i);
    //    }
    //}
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
    //public void SetList(ItemDataBase itemDataBase, int index)
    //{
    //    //프리펩을 설정해줄겁니다.
    //    var shopPrefab = shopItemList[index].GetComponent<ShopItem>();

    //    shopPrefab.Init(itemDataBase);
    //    if (itemDataBase.isSold)
    //    {
    //        shopPrefab.Sold();
    //    }
    //}
    //public void OnBuyButton(int index)
    //{
    //    var shopItem = GameData.userData.shopItemList[index];
    //    var cheak = GameData.userData.shopItemList.Count < GameData.userData.maxItem;
    //    //최대 아이템 갯수를 넘어서지 못하게 막아줍니다.
    //    if (!cheak)
    //    {
    //        return;
    //    }
    //    //구매를 할건데 돈이 부족하면 돈이 부족합니다를 띄우고 리턴해줄겁니다.
    //    if (shopItem.dataTable.price > GameData.userData.gold)
    //    {
    //        notEnoughGold.SetActive(true);
    //        return;
    //    }

    //    //구매를 할거예요 as 를 이용하여 아이템이 어떤 아이템인지 구분하여 만들어둔 AddItem 을 사용하여 줍니다.
    //    GameData.userData.gold -= shopItem.dataTable.price;
    //    shopItem.isSold = true;
    //    var isConsum = GameData.userData.shopItemList[index] as ConsumableItemData;
    //    if (isConsum != null)
    //    {
    //        GameData.userData.AddItem(isConsum.dataTable.id, ItemType.Consumable, isConsum.count);

    //        //test
    //        GameData.userData.SaveUserData(1);
    //    }


    //    //구매를 완료하면 sold out을 띄워줄겁니다.
    //    var shopPrefab = shopItemList[index].GetComponent<ShopItem>();
    //    shopPrefab.Sold();
    //}

    
    //public void OnNotEnoughtOkay()
    //{
    //    notEnoughGold.SetActive(false);
    //}
    //public void OnItemInfo(int index)
    //{
    //    info.gameObject.SetActive(true);
    //    info.Init(index);
    //}
    ////test
    //public void GetGold()
    //{
    //    GameData.userData.gold += 100;
    //}
}
