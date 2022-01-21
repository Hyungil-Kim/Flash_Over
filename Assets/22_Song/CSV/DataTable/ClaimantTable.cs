using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClaimantData : TableDataBase
{
    public int hp;
    public int lung;
    public int move;
    public int state;
    public int weight;
}

public class ClaimantTable : MyDataTableBase<ClaimantData>
{
    public ClaimantTable() : base("ClaimantDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new ClaimantData();
            tableData.id = table["ID"];
            tableData.hp = int.Parse(table["HP"]);
            tableData.lung = int.Parse(table["LUNG"]);
            tableData.move = int.Parse(table["MOVE"]);
            tableData.state = int.Parse(table["STATE"]);
            tableData.weight = int.Parse(table["WEIGHT"]);

            tables.Add(tableData);
        }
    }
}
