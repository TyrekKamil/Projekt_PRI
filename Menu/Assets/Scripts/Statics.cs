using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Statics 
{
    public static bool canSwitchLever = true;
    public static bool sceneWasLeft = false;
    public static Vector3 recentPlayerPosition;

    public static void SavePlayerData(PlayerUIUpdates player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        }
        else
        {
            Debug.LogError("Saved file not found in " + path);
            return null;
        }
    }
}
