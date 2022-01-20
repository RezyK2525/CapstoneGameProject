using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(int slotNumber, PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "save" + slotNumber + ".sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        Debug.Log("Save File");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData loadgameState(int slotNumber)
    {
        string path = Application.persistentDataPath + "save" + slotNumber + ".sav";
        if (File.Exists(path))
        {
            Debug.Log("Load File");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static bool saveExists(int slotNumber)
    {
        string path = Application.persistentDataPath + "save" + slotNumber + ".sav";
        return File.Exists(path);
    }
    public static void DeleteSave(int slotNumber)
    {
        string path = Application.persistentDataPath + "save" + slotNumber + ".sav";

        // check if file exists
        if (File.Exists(path))
        {
            Debug.Log(path + " file exists, deleting...");
            File.Delete(path);
            // RefreshEditorProjectWindow();
        }
        else
        {
            Debug.Log("no " + path + " file exists");
        }
    }
}