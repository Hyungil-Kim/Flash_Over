using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopData : MySaveData
{
    public List<ItemTableDataBase> shopItemList
        = new List<ItemTableDataBase>();
    public int shopLevel;
    public DateTime shopTime;

    public void SaveShop()
    {
        MySaveLoadSystem<ShopData>.Save(GameData.shopData, SaveDataType.Shop);
    }
    public void LoadShop()
    {
        GameData.shopData = MySaveLoadSystem<ShopData>.Load(SaveDataType.Shop);
        if (GameData.shopData == null)
        {
            GameData.shopData = new ShopData();
        }
        if (GameData.shopData.shopItemList != null)
        {
            foreach (var item in GameData.shopData.shopItemList)
            {
                item.iconSprite = Resources.Load<Sprite>($"Sprites/Icons/{item.iconID}");
            }
        }
    }
}
