using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopChaPrefab : MonoBehaviour
{
    //public Image icon;
    public RawImage icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI chaGrade;
    public TextMeshProUGUI chaClass;
    public CharacterData character;
    public void SetValue(CharacterData cd, int index)
    {
        icon.texture = Resources.Load<RenderTexture>($"Icon/icon {index}");
        character = cd;
        chaName.text = cd.characterName;
        chaGrade.text = cd.characterGrade;
        chaClass.text = cd.characterClass;
    }
    public void Hire()
    {
        var cheak = GameData.userData.characterList.Count < GameData.userData.maxCharacter;
        if (!cheak)
        {
            return;
        }
        if (GameData.userData.gold < 1000)
        {
            GetComponentInParent<CharacterShop>().needGold.SetActive(true);
            return;
        }
        
        gameObject.SetActive(false);
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

        character.isHire = true;
        GetComponentInParent<CharacterShop>().hireInfo.Init(null);
        
        GameData.userData.gold -= 1000;
        //test
        //GameData.userData.SaveUserData(1);
    }
}
