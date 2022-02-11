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
    List<GameObject> uiCharacterList = new List<GameObject>();
    public GameObject uiCharacter;

    private void Awake()
    {
        maxChaList = MyDataTableMgr.menuTable.GetTable(GameData.userData.chaShopData.countUpgrade).CS1Count;
        var uiCharacters = GameObject.FindGameObjectsWithTag("UICharacter");
        foreach (var uicharacter in uiCharacters)
        {
            uiCharacterList.Add(uicharacter);
        }
            
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
        gold.text = $"골드 : {GameData.userData.gold}";
    }
    public void ShopListUpdate()
    {
        GameData.userData.shopChaList.Clear();
        for (int i = 0; i < maxChaList; i++)
        {
            CharacterData cd = new CharacterData();
            //가중치 등급 뽑기
            var gradeWeight = MyDataTableMgr.itemGradeTable.GetTable(GameData.userData.chaShopData.gradeUpgrade);
            var grade = new WeightList<ItemGrade>();
            grade.AddGrade(ItemGrade.Normal, gradeWeight.normal);
            grade.AddGrade(ItemGrade.Rare, gradeWeight.rare);
            grade.AddGrade(ItemGrade.Unique, gradeWeight.unique);
            grade.AddGrade(ItemGrade.Special, gradeWeight.special);

            //등급별 아이템 뽑기 !
            var characterGrade = grade.GetRandomGrade();

            cd.NewSetCharacter((int)characterGrade);
            

            var prefab = shopChaList[i].GetComponent<ShopChaPrefab>();
            prefab.SetValue(cd, i);
            GameData.userData.shopChaList.Add(cd);
            shopChaList[i].SetActive(true);
            var prefabName = cd.prefabName;
            var go = Resources.Load<GameObject>($"Prefabs/Character/{prefabName}");
            var custom = go.GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
            var testSettings = custom.GetSetup();

            testSettings.HairColor = new float[4] { 1f, 1f, 1f, 1f };
            testSettings.selectedElements.Hair = Random.Range(-1, 15);
            testSettings.selectedElements.Hat = Random.Range(-1, 7);
            testSettings.selectedElements.Beard = Random.Range(-1, 9);
            testSettings.selectedElements.Item1 = Random.Range(-1, 3);
            testSettings.selectedElements.Accessory = Random.Range(-1,5);
            
            //testSettings.ApplyToCharacter(custom);
            cd.setupModel = testSettings;

            var model = uiCharacterList[i].GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
            cd.setupModel.ApplyToCharacter(model);
            uiCharacterList[i].GetComponent<UICharacter>().Init(i);
        }
    }

    public void SetList(CharacterData cd, int index)
    {
        var shopChaPrefab = shopChaList[index].GetComponent<ShopChaPrefab>();
        shopChaList[index].SetActive(true);
        shopChaPrefab.SetValue(cd, index);
    }

    public void OnChaButton(int index)
    {
        var cd = GameData.userData.shopChaList[currentPoint];
        currentPoint = index;
        hireInfo.Init(cd, index);


        //var custom = uiCharacter.GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
        //cd.setupModel.ApplyToCharacter(custom);
    }
    public void OnHireButton()
    {
        var cheak = GameData.userData.characterList.Count < MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Count;
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
