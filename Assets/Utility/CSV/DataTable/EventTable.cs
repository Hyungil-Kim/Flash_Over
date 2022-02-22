using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData : TableDataBase
{
    public int mapNumber;
    public int matchOrder;
    public int claimantNumber;
    public int hpUp;
    public int hpDown;
    public int lungUp;
    public int lungDown;
    public string text;
    public string answer1;
    public int answer1state;
    public string answer2;
    public int answer2state;

}

public class EventTable : MyDataTableBase<EventData>
{
    public EventTable() : base("EventDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new EventData();
            tableData.id = table["ID"];
            tableData.mapNumber = int.Parse(table["MAP"]);
            tableData.matchOrder = int.Parse(table["ORDER"]);
            tableData.claimantNumber = int.Parse(table["CLAIMANT"]);
            tableData.hpUp = int.Parse(table["HPUP"]);
            tableData.hpDown = int.Parse(table["HPDOWN"]);
            tableData.lungUp = int.Parse(table["LUNGUP"]);
            tableData.lungDown = int.Parse(table["LUNGDOWN"]);
            tableData.text = table["TEXT"];
            tableData.answer1 = table["ANSWER1"];
            tableData.answer1state = int.Parse(table["ANSWER1STATE"]);
            tableData.answer2 = table["ANSWER2"];
            tableData.answer2state = int.Parse(table["ANSWER2STATE"]);

            tables.Add(tableData);
        }
    }
}