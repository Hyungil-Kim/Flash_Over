using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int maxItemList;
    public float initSecondTime;
    public GameObject shopItemPrefab;
    public GameObject content;
    List<GameObject> shopItemList = new List<GameObject>();

    private void Start()
    {
        //�����ð����� �Ҷ� �ʿ�
        //GameData.shopData.LoadShop();
        //if(GameData.shopData.shopTime == default)
        //{
        //    GameData.shopData.shopTime = System.DateTime.Now;
        //}

        for (int i = 0; i < maxItemList; i++)
        {
            var index = i;
            var shopItem = Instantiate(shopItemPrefab, content.transform);
            var shopItemScript = shopItem.GetComponent<ShopItem>();
            shopItemScript.buyButton.onClick.AddListener(() => OnBuyButton(index));
            shopItemList.Add(shopItem);
        }

        if(GameData.shopData.shopItemList.Count == 0)
        {
            ShopListUpdate();
        }
        else
        {
            for (int i = 0; i < maxItemList; i++)
            {
                SetList(GameData.userData.shopItemList[i], i);
            }
        }
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


        //test��
        // �ڵ������ؾ߰���
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameData.shopData.SaveShop();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(GameData.userData.shopTime);
        }
        //������Ʈ ������Ʈ �Ǵ� �κ� ���� ���߿� �����ؾ߰���
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShopListUpdate();
        }
    }
    public void ShopListUpdate()
    {
        GameData.userData.shopItemList.Clear();
        for (int i = 0; i < maxItemList; i++)
        {
            //����ġ ��� �̱�
            var gradeWeight = MyDataTableMgr.itemGradeTable.GetTable(GameData.userData.shopLevel);
            var itemGrade = new WeightList<ItemGrade>();
            itemGrade.AddGrade(ItemGrade.Normal, gradeWeight.normal);
            itemGrade.AddGrade(ItemGrade.Rare, gradeWeight.rare);
            itemGrade.AddGrade(ItemGrade.Unique, gradeWeight.unique);
            itemGrade.AddGrade(ItemGrade.Special, gradeWeight.special);

            var grade = itemGrade.GetRandomGrade();

            //��޺� ������ �̱�
            var randomType = (ItemType)Random.Range(0, (int)ItemType.max);
            ItemTableDataBase gradeItem = null;
            switch (randomType)
            {
                case ItemType.Consumable:
                    gradeItem = GetRandomConsumItem(grade);
                    break;
                case ItemType.Weapon:
                    gradeItem = GetRandomWeaponItem(grade);
                    break;
                default:
                    break;
            }

            //���� ���������۸���Ʈ �߰�
            GameData.userData.shopItemList.Add(gradeItem);
            SetList(gradeItem, i);
        }
    }
    public ItemTableDataBase GetRandomConsumItem(ItemGrade grade)
    {
        int itemCount = 3;
        switch (grade)
        {
            case ItemGrade.Normal:
                var randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.normalItem.Count);
                var item = MyDataTableMgr.consumItemTable.normalItem[randomIndex];
                var itemGrade = StringToEnum.SToE<ItemGrade>(item.grade);
                item.count = ((int)ItemGrade.Normal - (int)itemGrade + 1) * itemCount;
                return item;
            case ItemGrade.Rare:
                randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.rareItem.Count);
                item = MyDataTableMgr.consumItemTable.rareItem[randomIndex];
                itemGrade = StringToEnum.SToE<ItemGrade>(item.grade);
                item.count = ((int)ItemGrade.Rare - (int)itemGrade + 1) * itemCount;
                return item;
            case ItemGrade.Unique:
                randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.uniqueItem.Count);
                item = MyDataTableMgr.consumItemTable.uniqueItem[randomIndex];
                itemGrade = StringToEnum.SToE<ItemGrade>(item.grade);
                item.count = ((int)ItemGrade.Unique - (int)itemGrade + 1) * itemCount;
                return item;
            case ItemGrade.Special:
                randomIndex = Random.Range(0, MyDataTableMgr.consumItemTable.specialItem.Count);
                item = MyDataTableMgr.consumItemTable.specialItem[randomIndex];
                itemGrade = StringToEnum.SToE<ItemGrade>(item.grade);
                item.count = ((int)ItemGrade.Special - (int)itemGrade + 1) * itemCount;
                return item;
            default:
                return null;
        }
    }
    public ItemTableDataBase GetRandomWeaponItem(ItemGrade grade)
    {
        int randomIndex = 0;
        switch (grade)
        {
            case ItemGrade.Normal:
                randomIndex = Random.Range(0, MyDataTableMgr.weaponTable.normalItem.Count);
                return MyDataTableMgr.weaponTable.normalItem[randomIndex];
            case ItemGrade.Rare:
                randomIndex = Random.Range(0, MyDataTableMgr.weaponTable.rareItem.Count);
                return MyDataTableMgr.weaponTable.rareItem[randomIndex];
            case ItemGrade.Unique:
                randomIndex = Random.Range(0, MyDataTableMgr.weaponTable.uniqueItem.Count);
                return MyDataTableMgr.weaponTable.uniqueItem[randomIndex];
            case ItemGrade.Special:
                randomIndex = Random.Range(0, MyDataTableMgr.weaponTable.specialItem.Count);
                return MyDataTableMgr.weaponTable.specialItem[randomIndex];
            default:
                return null;
        }
    }
    public void SetList(ItemTableDataBase itemDataBase, int index)
    {
        var shopPrefab = shopItemList[index].GetComponent<ShopItem>();

        shopPrefab.image.sprite = itemDataBase.iconSprite;
        shopPrefab.price.text = "100";
        shopPrefab.itemName.text = itemDataBase.itemName;
        shopPrefab.count.text = itemDataBase.count.ToString("D3");
    }
    public void OnBuyButton(int index)
    {
        var isWeapon = GameData.userData.shopItemList[index] as WeaponTableData;
        var isConsum = GameData.userData.shopItemList[index] as ConsumableItemTableData;
        if(isWeapon != null)
        {
            GameData.userData.AddItem(isWeapon.id, ItemType.Weapon, isWeapon.count);

            //test
            GameData.userData.SaveUserData(1);
        }
        else if(isConsum != null)
        {
            GameData.userData.AddItem(isConsum.id, ItemType.Consumable, isConsum.count);

            //test
            GameData.userData.SaveUserData(1);
        }
    }

    public void OnShopLevelUpButton()
    {
        GameData.userData.shopLevel = Mathf.Clamp(GameData.userData.shopLevel + 1, 0, MyDataTableMgr.itemGradeTable.tables.Count-1);
    }
}
