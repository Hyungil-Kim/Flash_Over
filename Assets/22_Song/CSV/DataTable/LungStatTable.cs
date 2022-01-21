using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LungStatTableData : StatTableDataBase
{

}
[System.Serializable]
public class LungStatTable : MyDataTableBase<LungStatTableData>
{
    public LungStatTable() : base("LungStatTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new LungStatTableData();
            tableData.id = table["ID"];
            tableData.increaseStat = int.Parse(table["INCREASESTAT"]);
            tableData.maxexp = int.Parse(table["MAXEXP"]);
            tables.Add(tableData);
        }
    }
}