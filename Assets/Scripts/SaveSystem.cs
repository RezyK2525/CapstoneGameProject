using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SavePlayer(int slotNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "save" + slotNumber + ".sav";
    }
    public bool saveExists(int slotNumber)
    {
        return false;
    }
    public void load()
    {

    }
}