using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum CharacterShopUpgrade
{
    Count,
    Grade,
    Characteristic
}
public class CharacterShopUpgradeData
{
    public int countUpgrade;
    public int gradeUpgrade;
    public int characteristicUpgrade;
}
public class CharacterShop : MonoBehaviour
{
    public HireInfo hireInfo;
    public int maxChaList;
    public GameObject shopChaPrefab;
    public GameObject content;
    public GameObject needGold;
    public TextMeshProUGUI gold;

    private int currentPoint;
    List<GameObject> shopChaList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxChaList; i++)
        {
            var index = i;
            var shopCha = Instantiate(shopChaPrefab, content.transform);
            var button = shopCha.GetComponent<Button>();
            button.onClick.AddListener(()=>OnChaButton(index));
            shopCha.SetActive(false);
            shopChaList.Add(shopCha);
        }
        if (GameData.userData.shopChaList.Count == 0)
        {
            ShopListUpdate();
        }
        else
        {
            for (int i = 0; i < maxChaList; i++)
            {
                if (GameData.userData.shopChaList[i].isHire == false)
                {
                    SetList(GameData.userData.shopChaList[i], i);
                }
            }
        }
        hireInfo.Init(null);
    }
    private void OnEnable()
    {
        
    }
    private void Update()
    {
        gold.text = $"°ñµå : {GameData.userData.gold}";
    }
    public void ShopListUpdate()
    {
        GameData.userData.shopChaList.Clear();
        for (int i = 0; i < maxChaList; i++)
        {
            CharacterData cd = new CharacterData();
            cd.NewSetCharacter();
            var prefab = shopChaList[i].GetComponent<ShopChaPrefab>();
            prefab.SetValue(cd);
            GameData.userData.shopChaList.Add(cd);
            shopChaList[i].SetActive(true);
        }
    }

    public void SetList(CharacterData cd, int index)
    {
        var shopChaPrefab = shopChaList[index].GetComponent<ShopChaPrefab>();
        shopChaList[index].SetActive(true);
        shopChaPrefab.SetValue(cd);
    }

    public void OnChaButton(int index)
    {
        currentPoint = index;
        hireInfo.Init(GameData.userData.shopChaList[currentPoint]);
    }
    public void OnHireButton()
    {
        var cheak = GameData.userData.characterList.Count < GameData.userData.maxCharacter;
        if (!cheak)
        {
            return;
        }
        if (GameData.userData.gold < 1000)
        {
            needGold.SetActive(true);
            return;
        }
        if (currentPoint < maxChaList)
        {
            shopChaList[currentPoint].SetActive(false);
            var character = GameData.userData.shopChaList[currentPoint];
            GameData.userData.characterList.Add(character);
            var oxygenItem = new OxygenTankData(MyDataTableMgr.oxygenTankTable.GetTable(0));
            GameData.userData.oxygenTankList.Add(oxygenItem);
            character.EquipItem(oxygenItem, ItemType.OxygenTank);
            var bunkergearItem = new BunkerGearData(MyDataTableMgr.bunkerGearTable.GetTable(0));
            GameData.userData.bunkerGearList.Add(bunkergearItem);
            character.EquipItem(bunkergearItem, ItemType.BunkerGear);
            var hoseItem = new HoseData(MyDataTableMgr.hoseTable.GetTable(0));
            GameData.userData.hoseList.Add(hoseItem);
            character.EquipItem(hoseItem, ItemType.Hose);

            GameData.userData.shopChaList[currentPoint].isHire = true;
            currentPoint = maxChaList + 1;
            hireInfo.Init(null);
        }
        GameData.userData.gold -= 1000;
        //test
        //GameData.userData.SaveUserData(1);
    }

    public void OnTest()
    {
        ShopListUpdate();
    }
    public void CheckNeedGold()
    {
        needGold.SetActive(false);
    }
    public void Upgrade(int index)
    {
        var upgradeIndex = (CharacterShopUpgrade)index;
        switch (upgradeIndex)
        {
            case CharacterShopUpgrade.Count:
                GameData.userData.chaShopData.countUpgrade++;
                break;
            case CharacterShopUpgrade.Grade:
                GameData.userData.chaShopData.gradeUpgrade++;
                break;
            case CharacterShopUpgrade.Characteristic:
                GameData.userData.chaShopData.characteristicUpgrade++;
                break;
            default:
                break;
        }
    }

}
