using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static UserData userData = new UserData();

    public static ShopData shopData = new ShopData();
    

    //public static UserData UD
    //{
    //    get
    //    {
    //        userData.userName = "user";
    //        userData.consumableItemList
    //            = new List<ConsumableItemData>();
    //        for (int i = 0; i < 6; i++)
    //        {
    //            ConsumableItemData data
    //                = new ConsumableItemData();

    //            data.count = 1;
    //            var test = Random.Range(0, 5);
    //            var key = string.Concat("CON_",test.ToString("D4"));
    //            data.dataTable
    //                = MyDataTableMgr.itemTable.GetData("CON_0003");

    //            userData.consumableItemList.Add(data);
    //        }

    //        return userData;
    //    }
    //}
}
