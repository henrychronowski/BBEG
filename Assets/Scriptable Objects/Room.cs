using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[CreateAssetMenu(fileName ="NewRoom", menuName = "ScriptableObjects/Create Room", order = 1)]
[SerializeField]
public class Room : ScriptableObject
{
    // Currently barebones but eventually will contain variables such as room rarity, making scriptable objects worthwhile for it
    // Primarily used for dungeon generation, RoomInfo.cs will be attached to every roomObj containing info needed during gameplay
    public GameObject roomObj;
    public int rngWeight;
    public GameObject Load(Vector3 loadPos)
    {
        return Instantiate(roomObj, loadPos, Quaternion.identity);
    }

    public RoomInfo GetRoomInfo()
    {
        return roomObj.GetComponent<RoomInfo>();
    }

    private void OnEnable()
    {
        
    }
}
