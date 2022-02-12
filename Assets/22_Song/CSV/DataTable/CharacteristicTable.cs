using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicData : TableDataBase
{
    public string name;
    public string desc;

    public int turn;
    public float checkValue;
    public float increaseStat;
    public float decreaseStat;


    public bool bad;
    public bool physical;
    public bool psychological;
    
}
public class CharacteristicTable : MyDataTableBase<CharacteristicData>
{
    public CharacteristicTable() : base("ChracteristicDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new CharacteristicData();

            tableData.id = table["ID"];

            tableData.name = table["NAME"];
            tableData.desc = table["DESC"];

            tableData.turn = int.Parse(table["TURN"]);
            tableData.checkValue = float.Parse(table["CHECKVALUE"]);
            tableData.increaseStat = float.Parse(table["UPSTAT"]);
            tableData.decreaseStat = float.Parse(table["DOWNSTAT"]);

            tableData.bad = bool.Parse(table["BAD"]);
            tableData.physical = bool.Parse(table["PHYSICAL"]);
            tableData.psychological = bool.Parse(table["PSYCHOLOGICAL"]);   


            tables.Add(tableData);
        }
    }
}
