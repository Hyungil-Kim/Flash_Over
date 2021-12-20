using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class WeaponTableData : ItemTableDataBase
{
    //public string grade;
    public int str;
}
public class WeaponTable : MyDataTableBase<WeaponTableData>
{
    public List<WeaponTableData> normalItem = new List<WeaponTableData>();
    public List<WeaponTableData> rareItem = new List<WeaponTableData>();
    public List<WeaponTableData> uniqueItem = new List<WeaponTableData>();
    public List<WeaponTableData> specialItem = new List<WeaponTableData>();
    public WeaponTable() : base("WeaponDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new WeaponTableData();
            tableData.id = table["ID"];
            tableData.iconID = table["ICON_ID"];
            tableData.str = int.Parse(table["STR"]);
            tableData.grade = table["GRADE"];
            tableData.iconSprite = Resources.Load<Sprite>($"Sprites/Icons/{tableData.iconID}");
            tableData.count = 1;
            tables.Add(tableData);
        }

        normalItem = tables.Where((x) => x.grade == ItemGrade.Normal.ToString()).ToList();
        rareItem = tables.Where((x) => x.grade == ItemGrade.Rare.ToString()).ToList();
        uniqueItem = tables.Where((x) => x.grade == ItemGrade.Unique.ToString()).ToList();
        specialItem = tables.Where((x) => x.grade == ItemGrade.Special.ToString()).ToList();
    }
}
