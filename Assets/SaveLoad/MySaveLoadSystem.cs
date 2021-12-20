using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;

public static class MySaveLoadSystem<T> where T : MySaveData
{
    private static string SaveFileName = "savefile";

    public static void Save(T saveData, SaveDataType type, int slot = 1)
    {
        //string backUpPath = "Backup";
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
        try
        {
            string backUpPath = "Backup";
            using BsonWriter writerBackUp = new BsonWriter(File.Create(string.Concat(backUpPath, type.ToString(),slot.ToString())));
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writerBackUp, saveData);
                writerBackUp.Close();
                File.Copy(string.Concat(backUpPath, type.ToString(), slot.ToString()), string.Concat(SaveFileName, type.ToString(), slot.ToString()), true);
            }
            if (File.Exists(string.Concat(backUpPath, type.ToString(), slot.ToString())))
            {
                File.Delete(string.Concat(backUpPath, type.ToString(), slot.ToString()));
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
    }
    public static T Load(SaveDataType type, int slot = 1)
    {
        T loadData = null;

        if (File.Exists(string.Concat(SaveFileName, type.ToString(), slot.ToString())))
        {
            using (FileStream fs = File.Open(string.Concat(SaveFileName, type.ToString(), slot.ToString()), FileMode.Open))
            {
                BsonReader reader = new BsonReader(fs);
                JsonSerializer serializer = new JsonSerializer();
                loadData = serializer.Deserialize<T>(reader);
            }
        }
        return loadData;
    }
}
