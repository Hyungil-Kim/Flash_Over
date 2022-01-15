using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoData : TableDataBase
{
    public int level;
    public int survivor;
    public int rescuer;
    public string descreption;
}
public class StageInfoTable : MyDataTableBase<StageInfoData>
{
    public StageInfoTable() : base("StageInfoDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new StageInfoData();
            tableData.id = table["ID"];
            tableData.level = int.Parse(table["LEVEL"]);
            tableData.survivor = int.Parse(table["SURVIVOR"]);
            tableData.rescuer = int.Parse(table["RESCUER"]);
            tableData.descreption = table["DESCREPTION"];
            

            tables.Add(tableData);
        }
    }
}
