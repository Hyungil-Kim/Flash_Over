using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyDataTableBase<T> where T : TableDataBase
{
    public List<Dictionary<string, string>> tableList = new List<Dictionary<string, string>>();
    public List<T> tables = new List<T>();
    public MyDataTableBase(string path)
    {
        var csv = CSVReader.Read(path);
        foreach (var data in csv)
        {
            tableList.Add(data);
        }
    }
    public T GetTable(int index)
    {
        return tables[index];
    }
    public T GetTable(string id)
    {
        var table = tables.Find(x => x.id == id);
        return table;
    }
}
