using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="NewRoom", menuName = "ScriptableObjects/Rooms", order = 1)]
public class Room : ScriptableObject
{
    // Room requirements
    // - A scene containing the room geometry and art, enemies items etc
    // - Some way to access the exit locations so that we can line up exits with more rooms
    public Scene room;
    public RoomInfo roomInfo;

    public void Load()
    {
        // Not using async loading for now for simplicity's sake, come back to it later if necessary
        SceneManager.LoadScene(room.name, LoadSceneMode.Additive);
    }

    public RoomInfo GetRoomInfo()
    {
        return roomInfo;
    }

    private void OnEnable()
    {
        
    }
}
