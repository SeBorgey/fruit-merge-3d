using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static SavesData SavesData;
    private static readonly string _path = Application.persistentDataPath + "/FruitMerge3D.save";
    public static Action OnLoad;
    public static Action OnSave;

    public static void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(_path, FileMode.Create);
        binaryFormatter.Serialize(stream, SavesData);
        stream.Close();
        Debug.Log("Data saved.");
        OnSave?.Invoke();
    }

    public static void Load()
    {
        if (File.Exists(_path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);
            SavesData = binaryFormatter.Deserialize(stream) as SavesData;
            stream.Close();
            Debug.Log("Save file loaded!");
        }
        else
        {
            Debug.Log("Save file does'nt exist! New save file will be created.");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Create);
            SavesData = new SavesData();
            binaryFormatter.Serialize(stream, SavesData);
            stream.Close();
        }
        OnLoad?.Invoke();
    }
}
