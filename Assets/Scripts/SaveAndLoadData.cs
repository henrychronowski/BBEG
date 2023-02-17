using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Game will be saved using a binary data file
public class SaveAndLoadData : MonoBehaviour
{
    private PlayerData playerData;
    private PlayerCharacterManager manager;

    private string path = "";
    private string persistentPath = "";

    private void Start()
    {
        manager = GameObject.Find("PlayerCharacterManager").GetComponent<PlayerCharacterManager>();
        playerData = new PlayerData(manager);
        SetDataPath();
    }

    private void SetDataPath()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "Player.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Player.json";
    }
    public void SavePlayerData()
    {
        string savePath = path;
        Debug.Log("Saving to " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadPlayerData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
    }
}
