using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoomType
{
    Starting,
    Combat,
    Dangerous,
    Minion,
    Safe,
    Shop,
    BossLobby,
    Boss
}

[CreateAssetMenu(fileName ="NewRoom", menuName = "ScriptableObjects/Create Room", order = 1)]
[SerializeField]
public class Room : ScriptableObject
{
    // Currently barebones but eventually will contain variables such as room rarity, making scriptable objects worthwhile for it
    // Primarily used for dungeon generation, RoomInfo.cs will be attached to every roomObj containing info needed during gameplay
    public GameObject roomObj;
    public int rngWeight;
    public static GameObject LoadRoom(GameObject roomToLoad, Vector3 loadPos)
    {
        return Instantiate(roomToLoad, loadPos, Quaternion.identity);
    }

    public static RoomInfo GetRoomInfo(GameObject room)
    {
        return room.GetComponent<RoomInfo>();
    }

    private void OnEnable()
    {
        
    }
}
