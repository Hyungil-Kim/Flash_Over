using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireData : TableDataBase
{
    public int level;
    public int minhp;
    public int maxhp;
    public int dmg;
    public int increaseHp;
    public int increaseExp;
    public int atkrange;
    public int makeSmoke;
}
public class FireTable : MyDataTableBase<FireData>
{
    public FireTable() : base("FireDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new FireData();
            tableData.id = table["ID"];
            tableData.level = int.Parse(table["LEVEL"]);
            tableData.minhp = int.Parse(table["MINHP"]);
            tableData.maxhp = int.Parse(table["MAXHP"]);
            tableData.dmg = int.Parse(table["DMG"]);
            tableData.increaseHp = int.Parse(table["INCREASEHP"]);
            tableData.increaseExp = int.Parse(table["INCREASEEXP"]);
            tableData.atkrange = int.Parse(table["ATKRANGE"]);
            tableData.makeSmoke = int.Parse(table["MAKESMOKE"]);

            tables.Add(tableData);
        }
    }
}
