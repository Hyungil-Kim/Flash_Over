using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGradeData : TableDataBase
{
    public int normal;
    public int rare;
    public int unique;
    public int special;
}

public class ItemGradeTable : MyDataTableBase<ItemGradeData>
{
    public ItemGradeTable() : base("ItemGradeTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new ItemGradeData();

            tableData.id = table["ID"];
            tableData.normal = int.Parse(table["NORMAL"]);
            tableData.rare = int.Parse(table["RARE"]);
            tableData.unique = int.Parse(table["UNIQUE"]);
            tableData.special = int.Parse(table["SPECIAL"]);

            tables.Add(tableData);
        }
    }

}
