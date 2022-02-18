using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using UnityEngine;

public static class MySaveLoadSystem<T> where T : MySaveData
{
    private static string SaveFileName = "savefile";
    public static bool CheckSaveFile(SaveDataType type, int slot = 0)
    {
        var filePath = string.Concat(SaveFileName, type.ToString(), slot.ToString());
        return File.Exists(filePath);
    }
    public static void Save(T saveData, SaveDataType type, int slot = 1)
    {

        //using BsonWriter writerBackUp = new BsonWriter(File.Create(string.Concat(backUpPath, type.ToString(), slot.ToString())));
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.Serialize(writerBackUp, saveData);
        //    writerBackUp.Close();
        //    File.Copy(string.Concat(backUpPath, type.ToString(), slot.ToString()), string.Concat(SaveFileName, type.ToString(), slot.ToString()), true);
        //}
        //if (File.Exists(string.Concat(backUpPath, type.ToString(), slot.ToString())))
        //{
        //    File.Delete(string.Concat(backUpPath, type.ToString(), slot.ToString()));
        //}
        //using (StreamWriter writer = File.CreateText(string.Concat(SaveFileName, type.ToString(), slot.ToString())))
        //{
        //    writer.Write(JsonUtility.ToJson(saveData));
        //}

        //JsonSerializer serializer = new JsonSerializer();
        //var filePath = string.Concat(SaveFileName, type.ToString(), slot.ToString());
        //using (StreamWriter sw = new StreamWriter(filePath))
        //using (JsonWriter writer = new JsonTextWriter(sw))
        //{

        //    serializer.Serialize(writer, saveData);
        //}

        var filePath = string.Concat(SaveFileName, type.ToString(), slot.ToString());

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            //ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //Formatting = Formatting.Indented
        };
        //settings.Converters.Add(new StringEnumConverter());
        string strJson = JsonConvert.SerializeObject(saveData, settings);
        var data = JsonConvert.DeserializeObject<T>(strJson, settings);
        if (data != null)
        {
            File.WriteAllText(filePath, strJson);
        }


        //string strJson = JsonConvert.SerializeObject(saveData, settings);

        //File.WriteAllText(filePath, strJson);



        //try
        //{
        //    string backUpPath = "Backup";
        //    using BsonWriter writerBackUp = new BsonWriter(File.Create(string.Concat(backUpPath, type.ToString(),slot.ToString())));
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(writerBackUp, saveData);
        //        writerBackUp.Close();
        //        File.Copy(string.Concat(backUpPath, type.ToString(), slot.ToString()), string.Concat(SaveFileName, type.ToString(), slot.ToString()), true);
        //    }
        //    if (File.Exists(string.Concat(backUpPath, type.ToString(), slot.ToString())))
        //    {
        //        File.Delete(string.Concat(backUpPath, type.ToString(), slot.ToString()));
        //    }
        //}
        //catch (Exception e)
        //{
        //    UnityEngine.Debug.Log(e);
        //}
    }
    public static T Load(SaveDataType type, int slot = 1)
    {
        T loadData = null;

        //if (File.Exists(string.Concat(SaveFileName, type.ToString(), slot.ToString())))
        //{
        //    using (FileStream fs = File.Open(string.Concat(SaveFileName, type.ToString(), slot.ToString()), FileMode.Open))
        //    {
        //        BsonReader reader = new BsonReader(fs);
        //        JsonSerializer serializer = new JsonSerializer();
        //        loadData = serializer.Deserialize<T>(reader);
        //    }
        //}
        //var filePath = string.Concat(SaveFileName, type.ToString(), slot.ToString());
        //if (File.Exists(filePath))
        //{
        //    using (FileStream r = File.Open(filePath, FileMode.Open))
        //    {
        //        StreamReader reader = new StreamReader(r);

        //        JsonSerializer serializer = new JsonSerializer();
        //        loadData = (T)serializer.Deserialize(reader, typeof(T));
        //    }
        //}
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            //ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //Formatting = Formatting.Indented
        };
        //settings.Converters.Add(new StringEnumConverter());
        var filePath = string.Concat(SaveFileName, type.ToString(), slot.ToString());
        if (File.Exists(filePath))
        {
            string st = File.ReadAllText(filePath);
            loadData = JsonConvert.DeserializeObject<T>(st, settings);
        }

        return loadData;
    }
}
