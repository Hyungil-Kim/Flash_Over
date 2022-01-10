using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BunkerGearData : ItemDataBase
{
    public BunkerGearTableData bunkerGearData;
    public BunkerGearData(ItemTableDataBase itemDataTable, int itemCount = 1)
    {
        dataTable = itemDataTable;
        count = itemCount;
        bunkerGearData = dataTable as BunkerGearTableData;
    }
}
