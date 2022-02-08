using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoData : TableDataBase
{
    public string sceneOfFire;
    public int level;
    public int survivor;
    public int rescuer;
    public string descreption;
    public string map;
}
public class StageInfoTable : MyDataTableBase<StageInfoData>
{
    public StageInfoTable() : base("StageInfoDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new StageInfoData();
            tableData.id = table["ID"];
            //tableData.sceneOfFire = table["SCENEOFFIRE"];
            tableData.level = int.Parse(table["LEVEL"]);
            tableData.survivor = int.Parse(table["SURVIVOR"]);
            tableData.rescuer = int.Parse(table["RESCUER"]);
            tableData.descreption = table["DESCREPTION"];
            //tableData.map = table["MAP"];

            tables.Add(tableData);
        }
    }
}
