using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : TableDataBase
{
    public int exp;
    public float hp;
    public float weight;
    public bool water;
    public float waterWeight;
}
public class TileTable : MyDataTableBase<TileData>
{
    public TileTable() : base("TileDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new TileData();
            tableData.id = table["ID"];
            tableData.exp = int.Parse(table["EXP"]);
            tableData.hp = float.Parse(table["HP"]);
            tableData.weight = float.Parse(table["WEIGHT"]);
            tableData.water = bool.Parse(table["WATER"]);
            tableData.waterWeight = float.Parse(table["WATERWEIGHT"]);

            tables.Add(tableData);
        }
    }
}
