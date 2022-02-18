using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoData : TableDataBase
{
    public string sceneOfFire;
    public int category;
    public int level;
    public int survivor;
    public int rescuer;
    public int gold;
    public int exp;
    public int catergory;
    public string descreption;
    public string mission;
    public string map;
    
}
public class StageInfoTable : MyDataTableBase<StageInfoData>
{
    public StageInfoTable() : base("StageInfoDataTableTest")
    {
        foreach (var table in tableList)
        {
            var tableData = new StageInfoData();
            tableData.id = table["ID"];
            tableData.sceneOfFire = table["SCENEOFFIRE"];
            tableData.category = int.Parse(table["CATEGORY"]);
            tableData.level = int.Parse(table["LEVEL"]);
            tableData.survivor = int.Parse(table["SURVIVOR"]);
            tableData.rescuer = int.Parse(table["RESCUER"]);
            tableData.gold = int.Parse(table["GOLD"]);
            tableData.exp = int.Parse(table["EXP"]);
            tableData.descreption = table["DESCREPTION"];
            tableData.mission = table["MISSON"];
            tableData.map = table["MAP"];
            tableData.category = int.Parse(table["CATEGORY"]);
            tables.Add(tableData);
        }
    }
}
