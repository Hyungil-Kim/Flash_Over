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
    //    //�켱�����ϸ� �� �������������� �ƽ������۰�����ŭ ������ٰſ���
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
    //    //�ε� �� ������������ ������ ��������� ������Ʈ���ٰſ���
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
    //    //�����۸���Ʈ�� ����
    //    GameData.userData.shopItemList.Clear();
    //    for (int i = 0; i < maxItemCount; i++)
    //    {
    //        //����ġ ��� �̱�
    //        var gradeWeight = MyDataTableMgr.itemGradeTable.GetTable(GameData.userData.shopLevel);
    //        var itemGrade = new WeightList<ItemGrade>();
    //        itemGrade.AddGrade(ItemGrade.Normal, gradeWeight.normal);
    //        itemGrade.AddGrade(ItemGrade.Rare, gradeWeight.rare);
    //        itemGrade.AddGrade(ItemGrade.Unique, gradeWeight.unique);
    //        itemGrade.AddGrade(ItemGrade.Special, gradeWeight.special);

    //        //��޺� ������ �̱� !
    //        var grade = itemGrade.GetRandomGrade();
    //        var gradeItem = GetRandomConsumItem(grade);

    //        //���� ���������۸���Ʈ �߰�
    //        SetList(gradeItem, i);
    //    }
    //}
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
    //public void SetList(ItemDataBase itemDataBase, int index)
    //{
    //    //�������� �������̴ٰϴ�.
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
    //    //�ִ� ������ ������ �Ѿ�� ���ϰ� �����ݴϴ�.
    //    if (!cheak)
    //    {
    //        return;
    //    }
    //    //���Ÿ� �Ұǵ� ���� �����ϸ� ���� �����մϴٸ� ���� �������̴ٰϴ�.
    //    if (shopItem.dataTable.price > GameData.userData.gold)
    //    {
    //        notEnoughGold.SetActive(true);
    //        return;
    //    }

    //    //���Ÿ� �Ұſ��� as �� �̿��Ͽ� �������� � ���������� �����Ͽ� ������ AddItem �� ����Ͽ� �ݴϴ�.
    //    GameData.userData.gold -= shopItem.dataTable.price;
    //    shopItem.isSold = true;
    //    var isConsum = GameData.userData.shopItemList[index] as ConsumableItemData;
    //    if (isConsum != null)
    //    {
    //        GameData.userData.AddItem(isConsum.dataTable.id, ItemType.Consumable, isConsum.count);

    //        //test
    //        GameData.userData.SaveUserData(1);
    //    }


    //    //���Ÿ� �Ϸ��ϸ� sold out�� ����̴ٰϴ�.
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
