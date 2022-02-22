using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : TableDataBase
{
    public float hp;
    public float weight;
    public EndBurn endtype;
    public int dmg;
    public int range;
    public int def;
}
public class ObjTable : MyDataTableBase<ObjData>
{
    public ObjTable() : base("ObjDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new ObjData();
            tableData.id = table["ID"];
            tableData.hp = float.Parse(table["HP"]);
            tableData.weight = float.Parse(table["WEIGHT"]);
            tableData.endtype = StringToEnum.SToE<EndBurn>(table["ENDTYPE"]);
            tableData.dmg = int.Parse(table["DMG"]);
            tableData.range = int.Parse(table["RANGE"]);
            tableData.def = int.Parse(table["DEF"]);

            tables.Add(tableData);
        }
    }
}
