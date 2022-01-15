using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SmokeData : TableDataBase
{
    public int level;
    public int min;
    public int max;
    public int decreasevision;
}
public class SmokeTable : MyDataTableBase<SmokeData>
{
    public SmokeTable() : base("SmokeDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new SmokeData();
            tableData.id = table["ID"];
            tableData.level = int.Parse(table["LEVEL"]);
            tableData.min = int.Parse(table["MIN"]);
            tableData.max = int.Parse(table["MAX"]);
            tableData.decreasevision = int.Parse(table["DECREASEVISION"]);

            tables.Add(tableData);
        }
    }
}
