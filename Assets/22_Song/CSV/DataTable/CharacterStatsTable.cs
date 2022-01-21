using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsData : TableDataBase
{

    //public int min;
    //public int max;
    public int hpmax;
    public int strmax;
    public int lungmax;
    public int totalmin;
    public int totalmax;
    public int movemin;
    public int movemax;
    public int visionmin;
    public int visionmax;
    public int basehp;
    public int basestr;
    public int baselung;
    public int exp;
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
            
            tableData.hpmax = int.Parse(table["HPMAX"]);
            tableData.strmax = int.Parse(table["STRMAX"]);
            tableData.lungmax = int.Parse(table["LUNGMAX"]);
            tableData.totalmin = int.Parse(table["TOTALMIN"]);
            tableData.totalmax = int.Parse(table["TOTALMAX"]);
            tableData.movemin = int.Parse(table["MOVEMIN"]);
            tableData.movemax = int.Parse(table["MOVEMAX"]);
            tableData.visionmin = int.Parse(table["VISIONMIN"]);
            tableData.visionmax = int.Parse(table["VISIONMAX"]);
            tableData.basehp = int.Parse(table["BASEHP"]);
            tableData.basestr = int.Parse(table["BASESTR"]);
            tableData.baselung = int.Parse(table["BASELUNG"]);
            tableData.exp = int.Parse(table["EXP"]);
            
            tables.Add(tableData);
        }
    }
}
