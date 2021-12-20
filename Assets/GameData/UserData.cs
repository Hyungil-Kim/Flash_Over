using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MySaveData
{
    public int slot;
    public string userName;
    public List<WeaponData> weaponItemList
        = new List<WeaponData>();
    public List<ConsumableItemData> consumableItemList
        = new List<ConsumableItemData>();
    public List<CharacterData> characterList
        = new List<CharacterData>();

    public List<ItemTableDataBase> shopItemList
        = new List<ItemTableDataBase>();
    public int shopLevel;
    public DateTime shopTime;

    public List<CharacterData> shopChaList
        = new List<CharacterData>();

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
        if(GameData.userData.characterList != null)
            foreach (var character in GameData.userData.characterList)
            {
                character.StatInit();
            }
        if (GameData.userData.consumableItemList != null)
            foreach (var item in GameData.userData.consumableItemList)
            {
                item.dataTable.iconSprite = Resources.Load<Sprite>($"Sprites/Icons/{item.itemData.iconID}");
            }
        if (GameData.userData.weaponItemList != null)
            foreach (var item in GameData.userData.weaponItemList)
            {
                item.dataTable.iconSprite = Resources.Load<Sprite>($"Sprites/Icons/{item.weaponData.iconID}");
            }
        if(GameData.userData.shopItemList != null)
            foreach (var item in GameData.userData.shopItemList)
            {
                item.iconSprite = Resources.Load<Sprite>($"Sprites/Icons/{item.iconID}");
            }
    }
    public void AddItem(string id, ItemType type, int count = 1)
    {
        switch (type)
        {
            case ItemType.Consumable:
                var item = new ConsumableItemData(MyDataTableMgr.consumItemTable.GetTable(id));
                item.count = count;
                consumableItemList.Add(item);
                break;
            case ItemType.Weapon:
                var weaponItem = new WeaponData(MyDataTableMgr.weaponTable.GetTable(id));
                weaponItem.count = count;
                weaponItemList.Add(weaponItem);
                break;
            default:
                break;
        }
    }
}
    