using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class StrStatTableData : StatTableDataBase
{

}
[System.Serializable]
public class StrStatTable : MyDataTableBase<StrStatTableData>
{
    public StrStatTable() : base("StrStatTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new StrStatTableData();
            tableData.id = table["ID"];
            tableData.increaseStat = int.Parse(table["INCREASESTAT"]);
            tableData.maxexp = int.Parse(table["MAXEXP"]);
            tables.Add(tableData);
        }
    }
}