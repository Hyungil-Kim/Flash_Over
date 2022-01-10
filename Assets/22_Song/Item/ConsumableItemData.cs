using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConsumableItemData : ItemDataBase
{
    public ConsumableItemTableData itemData;

    public ConsumableItemData(ItemTableDataBase itemDataBase, int itemCount = 1)
    {
        dataTable = itemDataBase;
        itemData = dataTable as ConsumableItemTableData;
        count = itemCount;
    }
    //public ItemTableData Itemdata
    //{
    //    get
    //    {
    //        itemData = dataTable as ItemTableData;
    //        return dataTable as ItemTableData;
    //    }
    //}
}
