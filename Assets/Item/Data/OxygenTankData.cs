using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OxygenTankData : ItemDataBase
{
    public OxygenTankTableData oxygenTankData;

    public OxygenTankData(ItemTableDataBase itemDataTable, int itemCount = 1)
    {
        dataTable = itemDataTable;
        count = itemCount;
        oxygenTankData = dataTable as OxygenTankTableData;
    }
}
