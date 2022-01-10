using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HoseData : ItemDataBase
{
    public HoseTableData hoseData;
    public HoseData(ItemTableDataBase itemDataTable, int itemCount = 1)
    {
        dataTable = itemDataTable;
        count = itemCount;
        hoseData = dataTable as HoseTableData;
    }
}
