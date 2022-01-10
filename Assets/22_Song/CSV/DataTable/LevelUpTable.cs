using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpData : TableDataBase
{

    public double exp;
}
public class LevelUpTable : MyDataTableBase<LevelUpData>
{
    //List<CharacterStatsData> tables = new List<CharacterStatsData>();
    public LevelUpTable() : base("LevelUpTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new LevelUpData();

            tableData.id = table["ID"];
            tableData.exp = double.Parse(table["EXP"]);

            tables.Add(tableData);
        }
    }
}
