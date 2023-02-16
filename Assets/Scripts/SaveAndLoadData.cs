using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Game will be saved using a binary data file
public static class SaveAndLoadData
{
    public static void SavePlayerData(PlayerCharacterManager player)
    {
        BinaryFormatter format = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.squimb";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        format.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.squimb";
        if(File.Exists(path))
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = format.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save data not found at " + path);
            return null;
        }
    }
}
