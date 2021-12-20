using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConsumableItemData : ItemDataBase
{
    public ConsumableItemTableData itemData;

    public ConsumableItemData(ItemTableDataBase itemDataBase)
    {
        dataTable = itemDataBase;
        itemData = dataTable as ConsumableItemTableData;
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
