using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class HpStatTableData : StatTableDataBase
{

}
[System.Serializable]
public class HpStatTable : MyDataTableBase<HpStatTableData>
{
    public HpStatTable() : base("HpStatTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new HpStatTableData();
            tableData.id = table["ID"];
            tableData.increaseStat = int.Parse(table["INCREASESTAT"]);
            tableData.maxexp = int.Parse(table["MAXEXP"]);
            tables.Add(tableData);
        }
    }
}