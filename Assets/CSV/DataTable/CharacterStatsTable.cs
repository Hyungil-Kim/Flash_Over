using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsData : TableDataBase
{

    public int min;
    public int max;
}
public class CharacterStatsTable : MyDataTableBase<CharacterStatsData>
{
    //List<CharacterStatsData> tables = new List<CharacterStatsData>();
    public CharacterStatsTable() : base("CharacterStatsMinMax")
    {
        foreach (var table in tableList)
        {
            var tableData = new CharacterStatsData();

            tableData.id = table["ID"];
            tableData.min = int.Parse(table["MIN"]);
            tableData.max = int.Parse(table["MAX"]);
            
            tables.Add(tableData);
        }
    }
}
