using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MySaveData
{
    //상점레벨, 캐릭터 상점 레벨에 따른 데이터 테이블도 존재할테고 그에 따라 바꿔줘야하는데 일단 ! 그냥 박자
    public int maxCharacter = 30;
    public int maxItem = 30;

    public int gold;
    public int slot;
    public string userName;
    //public List<WeaponData> weaponItemList
    //    = new List<WeaponData>();

    public List<ConsumableItemData> consumableItemList
        = new List<ConsumableItemData>();

    public List<HoseData> hoseList
        = new List<HoseData>();
    public List<BunkerGearData> bunkerGearList
        = new List<BunkerGearData>();
    public List<OxygenTankData> oxygenTankList
        = new List<OxygenTankData>();

    public List<CharacterData> characterList
        = new List<CharacterData>();

    public List<ItemDataBase> shopItemList
        = new List<ItemDataBase>();

    public int shopLevel = 1;
    public DateTime shopTime;

    public List<CharacterData> shopChaList
        = new List<CharacterData>();

    public int trainingRoomLevel = 1;

    public int maxFireMan = 9;

    public Dictionary<int,CharacterData> fireManList
        = new Dictionary<int, CharacterData>();
    public void SaveUserData(int slot)
    {
        MySaveLoadSystem<UserData>.Save(GameData.userData, SaveDataType.PlayerData,slot);
    }
    public void LoadUserData(int slot)
    {
        GameData.userData = MySaveLoadSystem<UserData>.Load(SaveDataType.PlayerData,slot);
        if (GameData.userData == null)
        {
            GameData.userData = new UserData();
        }
        if (GameData.userData.characterList != null)
        {
            foreach (var character in GameData.userData.characterList)
            {
                character.StatInit();
            }
        }

    }
    public void AddItem(string id, ItemType type, int count = 1)
    {
        switch (type)
        {
            case ItemType.Hose:
                var hoseItem = new HoseData(MyDataTableMgr.hoseTable.GetTable(id), count);
                hoseList.Add(hoseItem);
                break;
            case ItemType.BunkerGear:
                var bukerGearItem = new BunkerGearData(MyDataTableMgr.bunkerGearTable.GetTable(id), count);
                bunkerGearList.Add(bukerGearItem);
                break;
            case ItemType.OxygenTank:
                var oxygenTankItem = new OxygenTankData(MyDataTableMgr.oxygenTankTable.GetTable(id), count);
                oxygenTankList.Add(oxygenTankItem);
                break;

            case ItemType.Consumable:
                var item = new ConsumableItemData(MyDataTableMgr.consumItemTable.GetTable(id),count);
                //item.count = count;
                var index = consumableItemList.FindIndex((x) => x.dataTable.id == id);
                if(index == -1)
                {
                    consumableItemList.Add(item);
                }
                else
                {
                    consumableItemList[index].count += count;
                }
                break;
            //case ItemType.Weapon:
            //    var weaponItem = new WeaponData(MyDataTableMgr.weaponTable.GetTable(id),count);
            //    //weaponItem.count = count;
            //    weaponItemList.Add(weaponItem);
            default:
                break;
        }
    }
    public void RemoveItem(ItemDataBase itemData, ItemType type, int count = 1)
    {
        switch (type)
        {
            case ItemType.Hose:
                var hoseItem = hoseList.Find((x) => x == itemData);
                hoseItem.count -= count;
                if (hoseItem.count == 0)
                {
                    hoseList.Remove(hoseItem);
                }
                break;
            case ItemType.BunkerGear:
                var bunkerGearItem = bunkerGearList.Find((x) => x == itemData);
                bunkerGearItem.count -= count;
                if (bunkerGearItem.count == 0)
                {
                    bunkerGearList.Remove(bunkerGearItem);
                }
                break;
            case ItemType.OxygenTank:
                var oxygenTankItem = oxygenTankList.Find((x) => x == itemData);
                oxygenTankItem.count -= count;
                if (oxygenTankItem.count == 0)
                {
                    oxygenTankList.Remove(oxygenTankItem);
                }
                break;
            //case ItemType.Consumable:
            //    var item = consumableItemList.Find((x) => x == itemData);
            //    item.count -= count;
            //    if(item.count == 0)
            //    {
            //        consumableItemList.Remove(item);
            //    }
            //    break;
            //case ItemType.Weapon:
            //    var weaponItem = weaponItemList.Find((x) => x == itemData);
            //    weaponItem.count -= count;
            //    if(weaponItem.count == 0)
            //    {
            //        weaponItemList.Remove(weaponItem);
            //    }
            //    break;
            //case ItemType.max:
            //    break;
            default:
                break;
        }
    }
}
    