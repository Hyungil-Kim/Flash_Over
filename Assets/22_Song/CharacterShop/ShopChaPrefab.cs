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
    public TextMeshProUGUI chaHp;
    public TextMeshProUGUI chaStr;
    public TextMeshProUGUI chaLung;
    public TextMeshProUGUI chaMove;
    public TextMeshProUGUI chaVision;

    public CharacterData character;

    public GameObject characteristicPrefab;
    public GameObject characteristicContent;
    public void SetValue(CharacterData cd, int index)
    {
        icon.texture = Resources.Load<RenderTexture>($"Icon/icon {index}");
        character = cd;
        chaName.text = $"이름 : {cd.characterName}";
        chaHp.text = $"체력 : {cd.totalStats.hp.stat}";
        chaStr.text = $"힘 : {cd.totalStats.str.stat}";
        chaLung.text = $"폐활량 : {cd.totalStats.lung.stat}";
        chaMove.text = $"이동력 : {cd.totalStats.move}";
        chaVision.text = $"시야 : {cd.totalStats.vision}";
        
        if (cd.characteristics.Count > 0)
        {
            foreach (var characteristic in cd.characteristics)
            {
                var newGo = Instantiate(characteristicPrefab, characteristicContent.transform);
                var prefab = newGo.GetComponent<CharacteristicPrefab>();
                prefab.Init(characteristic);

            }
        }
    }
    public void Hire()
    {
        var cheak = GameData.userData.characterList.Count < MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Count;
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
